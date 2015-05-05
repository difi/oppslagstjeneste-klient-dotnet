using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Felles.Security
{
    /// <summary>
    /// Enhances the core SignedXml provider with namespace agnostic query for Id elements.
    /// </summary>
    /// <remarks>
    /// From: http://stackoverflow.com/questions/5099156/malformed-reference-element-when-adding-a-reference-based-on-an-id-attribute-w
    /// </remarks>
    public class SignedXmlWithAgnosticId : SignedXml
    {
        public SignedXmlWithAgnosticId(XmlDocument xml)
            : base(xml)
        {
        }

        /// <param name="xml">The document containing the references to be signed.</param>
        /// <param name="certificate">The certificate containing the private key used for signing.</param>
        public SignedXmlWithAgnosticId(XmlDocument xml, X509Certificate2 certificate)
            : base(xml)
        {
            Initialize(xml, certificate);
        }

        protected void Initialize(XmlDocument xml, X509Certificate2 certificate)
        {
            // Makes sure the signingkey has a private key.
            if (!certificate.HasPrivateKey)
                throw new Exception(string.Format("Angitt sertifikat med fingeravtrykk {0} inneholder ikke en privatnøkkel. Dette er påkrevet for å signere xml dokumenter.", certificate.Thumbprint));

            SigningKey = certificate.PrivateKey;
        }


        public override XmlElement GetIdElement(XmlDocument doc, string id)
        {
            // Attemt to find id node using standard methods. If not found, attempt using namespace agnostic method.
            XmlElement idElem = base.GetIdElement(doc, id) ?? FindIdElement(doc, id);

            // Check to se if id element is within the signatures object node. This is used by ESIs Xml Advanced Electronic Signatures (Xades)
            if (idElem == null && (Signature != null && Signature.ObjectList != null))
            {
                foreach (DataObject dataObject in Signature.ObjectList)
                {
                    if (dataObject.Data == null || dataObject.Data.Count <= 0) continue;

                    foreach (XmlNode dataNode in dataObject.Data)
                    {
                        idElem = FindIdElement(dataNode, id);
                        if (idElem != null)
                        {
                            return idElem;
                        }
                    }
                }
            }

            return idElem;
        }

        private XmlElement FindIdElement(XmlNode node, string idValue)
        {
            XmlElement result = null;
            foreach (string s in new[] { "Id", "ID", "id" })
            {
                result = node.SelectSingleNode(string.Format("//*[@*[local-name() = '{0}'] = '{1}']", s, idValue)) as XmlElement;
                if (result != null)
                    break;
            }

            return result;
        }        
    }
}
