using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Felles.Utility.Security;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Security
{
    internal abstract class ResponseValidator
    {
        internal ResponseValidator(XmlDocument sentDocument, ResponseContainer responseContainer,
            X509Certificate2 xmlDekrypteringsSertifikat = null)
        {
            SentDocument = sentDocument;
            ResponseContainer = responseContainer;
            if (xmlDekrypteringsSertifikat != null)
                DecryptDocument(xmlDekrypteringsSertifikat);
        }

        internal ResponseContainer ResponseContainer { get; }

        public XmlDocument SentDocument { get; }

        private void DecryptDocument(X509Certificate2 decryptionSertificate)
        {
            var encryptedNode =
                ResponseContainer.EncryptedBody;
            if (encryptedNode == null)
                return;

            var encryptedXml = new EncryptedXml(ResponseContainer.Envelope);
            var encryptedData = new EncryptedData();
            encryptedData.LoadXml(encryptedNode);

            var aes = AesManaged(decryptionSertificate);

            encryptedXml.ReplaceData(encryptedNode, encryptedXml.DecryptData(encryptedData, aes));
        }

        private AesManaged AesManaged(X509Certificate2 decryptionSertificate)
        {
            var privateKey = decryptionSertificate.GetRSAPrivateKey();

            var aes = new AesManaged
            {
                Mode = CipherMode.CBC,
                KeySize = 256,
                Padding = PaddingMode.None,
                Key = privateKey.Decrypt(Convert.FromBase64String(ResponseContainer.Cipher), RSAEncryptionPadding.OaepSHA1)
            };
            return aes;
        }

        protected virtual void CheckTimestamp(TimeSpan timeSpan)
        {
            var timestampElement = ResponseContainer.TimestampElement;

            var created = DateTimeOffset.Parse(timestampElement["Created", Navnerom.WssecurityUtility10].InnerText);
            var expires = DateTimeOffset.Parse(timestampElement["Expires", Navnerom.WssecurityUtility10].InnerText);

            if (created > DateTimeOffset.Now.AddMinutes(5))
                throw new ValideringsException("Motatt melding har opprettelsetidspunkt mer enn 5 minutter inn i fremtiden." +
                                               created);
            if (created < DateTimeOffset.Now.Add(timeSpan.Negate()))
                throw new ValideringsException(
                    $"Motatt melding har opprettelsetidspunkt som er eldre enn {timeSpan.Minutes} minutter.");

            if (expires < DateTimeOffset.Now)
                throw new ValideringsException("Motatt melding har utgått på tid.");
        }

        /// <summary>
        ///     Sjekker at soap envelopen inneholder timestamp, body og messaging element med korrekt id og referanser i security
        ///     signaturen.
        /// </summary>
        protected void ValidateSignatureReferences(XmlElement signature, SignedXmlWithAgnosticId signedXml,
            string[] påkrevdeReferanser)
        {
            foreach (var påkrevdReferanse in påkrevdeReferanser)
            {
                var node = InneholderNode(påkrevdReferanse);
                var elementId = NodeFinnesISignaturElement(signature, node, påkrevdReferanse);

                IdNodeMatcher(signedXml, elementId, node, påkrevdReferanse);
            }
        }

        private void IdNodeMatcher(SignedXmlWithAgnosticId signedXml, string elementId, XmlNodeList nodes,
            string elementXPath)
        {
            var targetNode = signedXml.GetIdElement(ResponseContainer.Envelope, elementId);
            if (targetNode != nodes[0])
                throw new ValideringsException($"Signaturreferansen med id '{elementId}' må referere til node med sti '{elementXPath}'");
        }

        private string NodeFinnesISignaturElement(XmlElement signature, XmlNodeList nodes, string elementXPath)
        {
            var elementId = nodes[0].Attributes["wsu:Id"].Value;

            var references = signature.SelectNodes($"./ds:SignedInfo/ds:Reference[@URI='#{elementId}']", ResponseContainer.Nsmgr);
            if ((references == null) || (references.Count == 0))
            {
                throw new Exception($"Kan ikke finne påkrevet refereanse til element '{elementXPath}' i signatur fra meldingsformidler."
                );
            }
            if (references.Count > 1)
                throw new ValideringsException($"Påkrevet refereanse til element '{elementXPath}' kan kun forekomme én gang i signatur fra meldingsformidler. Ble funnet {references.Count} ganger.");
            return elementId;
        }

        private XmlNodeList InneholderNode(string elementXPath)
        {
            var nodes = ResponseContainer.Envelope.SelectNodes(elementXPath, ResponseContainer.Nsmgr);
            if ((nodes == null) || (nodes.Count == 0))
            {
                throw new ValideringsException($"Kan ikke finne påkrevet element '{elementXPath}' i svar fra meldingsformidler.");
            }

            if (nodes.Count > 1)
            {
                throw new ValideringsException($"Påkrevet element '{elementXPath}' kan kun forekomme én gang i svar fra meldingsformidler. Ble funnet {nodes.Count} ganger.");
            }

            return nodes;
        }

        protected void PerformSignatureConfirmation(XmlElement securityNode)
        {
            // Locate SignatureConfirmation element in response document
            var signatureConfirmationNode = securityNode.SelectSingleNode("./wsse11:SignatureConfirmation", ResponseContainer.Nsmgr);
            var signatureConfirmation = signatureConfirmationNode.Attributes["Value"].Value;

            // Locate sent signature
            var sentSignatureValueNode =
                SentDocument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/ds:Signature/ds:SignatureValue",
                    ResponseContainer.Nsmgr);
            var sentSignatureValue = sentSignatureValueNode.InnerText;

            // match against sent signature
            if (signatureConfirmation != sentSignatureValue)
                throw new ValideringsException($"Motatt signaturbekreftelse '{signatureConfirmation}' er ikke lik sendt signatur '{sentSignatureValue}'.");
        }
    }
}