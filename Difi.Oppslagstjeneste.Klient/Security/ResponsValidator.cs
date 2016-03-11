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
    public abstract class Responsvalidator
    {
        internal Responsvalidator(XmlDocument sendtDokument, ResponseContainer responseContainer,
            X509Certificate2 xmlDekrypteringsSertifikat = null)
        {
            SendtDokument = sendtDokument;
            ResponseContainer = responseContainer;
            if (xmlDekrypteringsSertifikat != null)
                DecryptDocument(xmlDekrypteringsSertifikat);
        }

        internal ResponseContainer ResponseContainer { get; }

        public XmlDocument SendtDokument { get; }

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
            var privateKey = decryptionSertificate.PrivateKey as RSACryptoServiceProvider;

            var aes = new AesManaged
            {
                Mode = CipherMode.CBC,
                KeySize = 256,
                Padding = PaddingMode.None,
                Key = privateKey.Decrypt(Convert.FromBase64String(ResponseContainer.Cipher), true)
            };
            return aes;
        }

        protected virtual void SjekkTimestamp(TimeSpan timeSpan)
        {
            var timestampElement = ResponseContainer.TimestampElement;

            var created = DateTimeOffset.Parse(timestampElement["Created", Navnerom.WssecurityUtility10].InnerText);
            var expires = DateTimeOffset.Parse(timestampElement["Expires", Navnerom.WssecurityUtility10].InnerText);

            if (created > DateTimeOffset.Now.AddMinutes(5))
                throw new ValidationException("Motatt melding har opprettelsetidspunkt mer enn 5 minutter inn i fremtiden." +
                                              created);
            if (created < DateTimeOffset.Now.Add(timeSpan.Negate()))
                throw new ValidationException(
                    string.Format("Motatt melding har opprettelsetidspunkt som er eldre enn {0} minutter.",
                        timeSpan.Minutes));

            if (expires < DateTimeOffset.Now)
                throw new ValidationException("Motatt melding har utgått på tid.");
        }

        /// <summary>
        ///     Sjekker at soap envelopen inneholder timestamp, body og messaging element med korrekt id og referanser i security
        ///     signaturen.
        /// </summary>
        protected void ValiderSignaturreferanser(XmlElement signature, SignedXmlWithAgnosticId signedXml,
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
                throw new ValidationException(string.Format("Signaturreferansen med id '{0}' må refererer til node med sti '{1}'",
                    elementId, elementXPath));
        }

        private string NodeFinnesISignaturElement(XmlElement signature, XmlNodeList nodes, string elementXPath)
        {
            var elementId = nodes[0].Attributes["wsu:Id"].Value;

            var references = signature.SelectNodes(
                string.Format("./ds:SignedInfo/ds:Reference[@URI='#{0}']", elementId), ResponseContainer.Nsmgr);
            if (references == null || references.Count == 0)
            {
                throw new Exception(
                    string.Format(
                        "Kan ikke finne påkrevet refereanse til element '{0}' i signatur fra meldingsformidler.",
                        elementXPath)
                    );
            }
            if (references.Count > 1)
                throw new ValidationException(
                    string.Format(
                        "Påkrevet refereanse til element '{0}' kan kun forekomme én gang i signatur fra meldingsformidler. Ble funnet {1} ganger.",
                        elementXPath, references.Count)
                    );
            return elementId;
        }

        private XmlNodeList InneholderNode(string elementXPath)
        {
            var nodes = ResponseContainer.Envelope.SelectNodes(elementXPath, ResponseContainer.Nsmgr);
            if (nodes == null || nodes.Count == 0)
            {
                throw new ValidationException(string.Format(
                    "Kan ikke finne påkrevet element '{0}' i svar fra meldingsformidler.", elementXPath));
            }

            if (nodes.Count > 1)
            {
                throw new ValidationException(
                    string.Format(
                        "Påkrevet element '{0}' kan kun forekomme én gang i svar fra meldingsformidler. Ble funnet {1} ganger.",
                        elementXPath, nodes.Count)
                    );
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
                SendtDokument.SelectSingleNode("/env:Envelope/env:Header/wsse:Security/ds:Signature/ds:SignatureValue",
                    ResponseContainer.Nsmgr);
            var sentSignatureValue = sentSignatureValueNode.InnerText;

            // match against sent signature
            if (signatureConfirmation != sentSignatureValue)
                throw new ValidationException(string.Format("Motatt signaturbekreftelse '{0}' er ikke lik sendt signatur '{1}'.",
                    signatureConfirmation, sentSignatureValue));
        }
    }
}