using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Felles.Security
{
    public enum SoapVersion
    {
        Soap11,
        Soap12
    }

    public abstract class ResponseValidator
    {
        protected XmlNamespaceManager nsmgr;
        
        public XmlDocument ResponseDocument { get; private set; }

        protected XmlDocument SentEnvelope { get; private set; }

        protected XmlElement HeaderSecurityElement {get; private set;}
        
        protected XmlElement HeaderSignatureElement {get; private set;}
               
        public ResponseValidator(System.IO.Stream stream, SoapVersion version, XmlDocument sentEnvelope, X509Certificate2 xmlDekrypteringsSertifikat = null)
        {
            SentEnvelope = sentEnvelope;

            ResponseDocument = new XmlDocument();
            ResponseDocument.Load(stream);

            nsmgr = new XmlNamespaceManager(ResponseDocument.NameTable);
            if (version == SoapVersion.Soap11)
                nsmgr.AddNamespace("env", Navnerom.SoapEnvelope);
            else
                nsmgr.AddNamespace("env", Navnerom.SoapEnvelopeEnv12);
            nsmgr.AddNamespace("wsse", Navnerom.WssecuritySecext10);
            nsmgr.AddNamespace("ds", Navnerom.XmlDsig);            
            nsmgr.AddNamespace("xenc", Navnerom.xenc); 
            nsmgr.AddNamespace("wsse11", Navnerom.WssecuritySecext11);
            nsmgr.AddNamespace("wsu", Navnerom.WssecurityUtility10);

            HeaderSecurityElement = ResponseDocument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security", nsmgr) as XmlElement;
            HeaderSignatureElement = HeaderSecurityElement.SelectSingleNode("./ds:Signature", nsmgr) as XmlElement;

            if (xmlDekrypteringsSertifikat != null)
                DecryptDocument(xmlDekrypteringsSertifikat);
        }

        private void DecryptDocument(X509Certificate2 decryptionSertificate)
        {
            // TODO: Use more of .NET native code.
            var encryptedNode = ResponseDocument.SelectSingleNode("/env:Envelope/env:Body/xenc:EncryptedData", nsmgr) as XmlElement;
            if (encryptedNode == null)
                return;
            
            EncryptedXml enc = new EncryptedXml(ResponseDocument);
            
            EncryptedData ed = new EncryptedData();
            ed.LoadXml(encryptedNode);

            var privateKey = decryptionSertificate.PrivateKey as RSACryptoServiceProvider;
            var cipher = ResponseDocument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/xenc:EncryptedKey/xenc:CipherData/xenc:CipherValue", nsmgr).InnerText;

            var test = enc.GetDecryptionKey(ed, "http://www.w3.org/2001/04/xmlenc#aes256-cbc");

            AesManaged aes = new AesManaged();
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            aes.Padding = PaddingMode.None;
            aes.Key = privateKey.Decrypt(System.Convert.FromBase64String(cipher), true);

            enc.ReplaceData(encryptedNode, enc.DecryptData(ed, aes));

        }


        protected void SjekkTimestamp(TimeSpan timeSpan)
        {
            var timestampElement = HeaderSecurityElement.SelectSingleNode("./wsu:Timestamp", nsmgr);

            var created = DateTimeOffset.Parse(timestampElement["Created", Navnerom.WssecurityUtility10].InnerText);
            var expires = DateTimeOffset.Parse(timestampElement["Expires", Navnerom.WssecurityUtility10].InnerText);

            if (created > DateTimeOffset.Now.AddMinutes(5))
                throw new Exception("Motatt melding har opprettelsetidspunkt mer enn fem minutter inn i fremtiden." + created.ToString());
            if (created < DateTimeOffset.Now.Add(timeSpan.Negate()))
                throw new Exception(string.Format("Motatt melding har opprettelsetidspunkt som er eldre enn {0} minutter.", timeSpan.Minutes));

            if (expires < DateTimeOffset.Now)
                throw new Exception("Motatt melding har utgått på tid.");

        }

        /// <summary>
        /// Sjekker at soap envelopen inneholder timestamp, body og messaging element med korrekt id og referanser i security signaturen.
        /// </summary>
        protected void ValiderSignaturReferences(XmlElement signature, SignedXmlWithAgnosticId signedXml, string[] requiredReferences)
        {
            foreach (var elementXPath in requiredReferences)
            {
                // Sørg for at svar inneholde påkrevede noder.
                var nodes = ResponseDocument.SelectNodes(elementXPath, nsmgr);
                if (nodes == null || nodes.Count == 0)
                    throw new Exception(string.Format("Kan ikke finne påkrevet element '{0}' i svar fra meldingsformidler.", elementXPath));
                if (nodes.Count > 1)
                    throw new Exception(string.Format("Påkrevet element '{0}' kan kun forekomme én gang i svar fra meldingsformidler. Ble funnet {1} ganger.", elementXPath, nodes.Count));

                // Sørg for at det finnes en refereanse til node i signatur element
                var elementId = nodes[0].Attributes["wsu:Id"].Value;

                var references = signature.SelectNodes(string.Format("./ds:SignedInfo/ds:Reference[@URI='#{0}']", elementId), nsmgr);
                if (references == null || references.Count == 0)
                    throw new Exception(string.Format("Kan ikke finne påkrevet refereanse til element '{0}' i signatur fra meldingsformidler.", elementXPath));
                if (references.Count > 1)
                    throw new Exception(string.Format("Påkrevet refereanse til element '{0}' kan kun forekomme én gang i signatur fra meldingsformidler. Ble funnet {1} ganger.", elementXPath, references.Count));

                // Sørg for at Id node matcher
                var targetNode = signedXml.GetIdElement(ResponseDocument, elementId);
                if (targetNode != nodes[0])
                    throw new Exception(string.Format("Signaturreferansen med id '{0}' må refererer til node med sti '{1}'", elementId, elementXPath));
            }
        }


        protected void PerformSignatureConfirmation(XmlElement securityNode)
        {
            // Locate SignatureConfirmation element in response document
            var signatureConfirmationNode = securityNode.SelectSingleNode("./wsse11:SignatureConfirmation", nsmgr);
            var signatureConfirmation = signatureConfirmationNode.Attributes["Value"].Value;

            // Locate sent signature
            var sentSignatureValueNode = SentEnvelope.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/ds:Signature/ds:SignatureValue", nsmgr);
            var sentSignatureValue = sentSignatureValueNode.InnerText;

            // match against sent signature
            if (signatureConfirmation != sentSignatureValue)
                throw new Exception(string.Format("Motatt signaturbekreftelse '{0}' er ikke lik sendt signatur '{1}'.", signatureConfirmation, sentSignatureValue));

        }

    }
}
