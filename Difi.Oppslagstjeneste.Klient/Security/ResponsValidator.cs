using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;

namespace Difi.Oppslagstjeneste.Klient.Security
{
    public abstract class Responsvalidator
    {
        protected XmlNamespaceManager Nsmgr;
        
        public XmlDocument MottattDokument { get; private set; }

        public XmlDocument SendtDokument { get; private set; }

        protected XmlElement HeaderSecurityElement {get; private set;}
        
        protected XmlElement HeaderSignatureElement {get; private set;}

        protected Responsvalidator(XmlDocument mottattDokument, SoapVersion version, XmlDocument sendtDokument, X509Certificate2 xmlDekrypteringsSertifikat = null)
        {
            SendtDokument = sendtDokument;
            MottattDokument = mottattDokument;

            Nsmgr = new XmlNamespaceManager(MottattDokument.NameTable);
            Nsmgr.AddNamespace("env", version == SoapVersion.Soap11 ? Navnerom.SoapEnvelope : Navnerom.SoapEnvelopeEnv12);
            Nsmgr.AddNamespace("wsse", Navnerom.WssecuritySecext10);
            Nsmgr.AddNamespace("ds", Navnerom.XmlDsig);            
            Nsmgr.AddNamespace("xenc", Navnerom.xenc); 
            Nsmgr.AddNamespace("wsse11", Navnerom.WssecuritySecext11);
            Nsmgr.AddNamespace("wsu", Navnerom.WssecurityUtility10);

            HeaderSecurityElement = MottattDokument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security", Nsmgr) as XmlElement;
            HeaderSignatureElement = HeaderSecurityElement.SelectSingleNode("./ds:Signature", Nsmgr) as XmlElement;

            if (xmlDekrypteringsSertifikat != null)
                DecryptDocument(xmlDekrypteringsSertifikat);
        }

        private void DecryptDocument(X509Certificate2 decryptionSertificate)
        {
            var encryptedNode = MottattDokument.SelectSingleNode("/env:Envelope/env:Body/xenc:EncryptedData", Nsmgr) as XmlElement;
            if (encryptedNode == null)
                return;
            
            var encryptedXml = new EncryptedXml(MottattDokument);
            var encryptedData = new EncryptedData();
            encryptedData.LoadXml(encryptedNode);

            var privateKey = decryptionSertificate.PrivateKey as RSACryptoServiceProvider;
            var cipher = MottattDokument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/xenc:EncryptedKey/xenc:CipherData/xenc:CipherValue", Nsmgr).InnerText;

            AesManaged aes = new AesManaged
            {
                Mode = CipherMode.CBC,
                KeySize = 256,
                Padding = PaddingMode.None,
                Key = privateKey.Decrypt(Convert.FromBase64String(cipher), true)
            };

            encryptedXml.ReplaceData(encryptedNode, encryptedXml.DecryptData(encryptedData, aes));
        }


        protected void SjekkTimestamp(TimeSpan timeSpan)
        {
            var timestampElement = HeaderSecurityElement.SelectSingleNode("./wsu:Timestamp", Nsmgr);

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
        protected void ValiderSignaturreferanser(XmlElement signature, SignedXmlWithAgnosticId signedXml, string[] påkrevdeReferanser)
        {
            foreach (var påkrevdReferanse in påkrevdeReferanser)
            {
                var node = InneholderNode(påkrevdReferanse);
                var elementId = NodeFinnesISignaturElement(signature, node, påkrevdReferanse);

                IdNodeMatcher(signedXml, elementId, node, påkrevdReferanse);
            }
        }

        private void IdNodeMatcher(SignedXmlWithAgnosticId signedXml, string elementId, XmlNodeList nodes, string elementXPath)
        {
            var targetNode = signedXml.GetIdElement(MottattDokument, elementId);
            if (targetNode != nodes[0])
                throw new Exception(string.Format("Signaturreferansen med id '{0}' må refererer til node med sti '{1}'",
                    elementId, elementXPath));
        }

        private string NodeFinnesISignaturElement(XmlElement signature, XmlNodeList nodes, string elementXPath)
        {
            var elementId = nodes[0].Attributes["wsu:Id"].Value;

            var references = signature.SelectNodes(string.Format("./ds:SignedInfo/ds:Reference[@URI='#{0}']", elementId), Nsmgr);
            if (references == null || references.Count == 0)
            { 
                throw new Exception(
                    string.Format("Kan ikke finne påkrevet refereanse til element '{0}' i signatur fra meldingsformidler.",elementXPath)
                    );
            }
            if (references.Count > 1)
                throw new Exception(
                    string.Format("Påkrevet refereanse til element '{0}' kan kun forekomme én gang i signatur fra meldingsformidler. Ble funnet {1} ganger.",elementXPath, references.Count)
                    );
            return elementId;
        }

        private XmlNodeList InneholderNode(string elementXPath)
        {
            var nodes = MottattDokument.SelectNodes(elementXPath, Nsmgr);
            if (nodes == null || nodes.Count == 0)
            {
                throw new Exception(string.Format("Kan ikke finne påkrevet element '{0}' i svar fra meldingsformidler.",elementXPath));
            }

            if (nodes.Count > 1)
            {
                throw new Exception(
                    string.Format("Påkrevet element '{0}' kan kun forekomme én gang i svar fra meldingsformidler. Ble funnet {1} ganger.", elementXPath, nodes.Count)
                    );
            }
                
            return nodes;
        }


        protected void PerformSignatureConfirmation(XmlElement securityNode)
        {
            // Locate SignatureConfirmation element in response document
            var signatureConfirmationNode = securityNode.SelectSingleNode("./wsse11:SignatureConfirmation", Nsmgr);
            var signatureConfirmation = signatureConfirmationNode.Attributes["Value"].Value;

            // Locate sent signature
            var sentSignatureValueNode = SendtDokument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/ds:Signature/ds:SignatureValue", Nsmgr);
            var sentSignatureValue = sentSignatureValueNode.InnerText;

            // match against sent signature
            if (signatureConfirmation != sentSignatureValue)
                throw new Exception(string.Format("Motatt signaturbekreftelse '{0}' er ikke lik sendt signatur '{1}'.", signatureConfirmation, sentSignatureValue));

        }
    }
}
