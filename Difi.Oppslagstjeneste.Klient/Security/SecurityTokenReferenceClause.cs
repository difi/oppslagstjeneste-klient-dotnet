using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Security
{
    public class SecurityTokenReferenceClause : KeyInfoClause
    {
        public SecurityTokenReferenceClause(string uri)
        {
            Uri = uri;
        }

        public SecurityTokenReferenceClause(X509Certificate2 certificate, string uri)
        {
            Certificate = certificate;
            Uri = uri;
        }

        public string Uri { get; set; }

        public X509Certificate2 Certificate { get; set; }

        internal EnvelopeSettings Settings { get; set; }

        public override XmlElement GetXml()
        {
            return GetXml(new XmlDocument {PreserveWhitespace = true});
        }

        public XmlElement GetTokenXml()
        {
            return GetTokenXml(new XmlDocument {PreserveWhitespace = true});
        }

        private XmlElement GetTokenXml(XmlDocument xmlDocument)
        {
            var xmlElement = xmlDocument.CreateElement("wsse", "BinarySecurityToken", Navnerom.WssecuritySecext10);
            xmlElement.SetAttribute("EncodingType",
                "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            xmlElement.SetAttribute("ValueType",
                "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
            var binIdAttribute = xmlDocument.CreateAttribute("wsu", "Id", Navnerom.WssecurityUtility10);
            binIdAttribute.Value = string.Format("{0}", Uri);
            xmlElement.SetAttributeNode(binIdAttribute);
            xmlElement.InnerText = Convert.ToBase64String(Certificate.RawData);

            return xmlElement;
        }

        private XmlElement GetXml(XmlDocument xmlDocument)
        {
            var element1 = xmlDocument.CreateElement("wsse", "SecurityTokenReference", Navnerom.WssecuritySecext10);

            var idAttribute = xmlDocument.CreateAttribute("wsu", "Id", Navnerom.WssecurityUtility10);
            idAttribute.Value = string.Format("STR-{0}", Guid.NewGuid());
            element1.SetAttributeNode(idAttribute);

            if (Certificate == null && !string.IsNullOrEmpty(Uri))
            {
                var element2 = xmlDocument.CreateElement("Reference", Navnerom.WssecuritySecext10);
                element2.SetAttribute("URI", string.Format("#{0}", Uri));
                element2.SetAttribute("ValueType",
                    "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
                element1.AppendChild(element2);
            }

            return element1;
        }

        public override void LoadXml(XmlElement element)
        {
        }
    }
}