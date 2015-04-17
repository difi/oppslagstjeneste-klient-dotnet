using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Felles.Security
{
    public class SecurityTokenReferenceClause : KeyInfoClause
    {
        public string Uri { get; set; }
        public X509Certificate2 Certificate { get; set; }

        public SecurityTokenReferenceClause(string uri)
        {
            Uri = uri;
        }

        public SecurityTokenReferenceClause(X509Certificate2 certificate)
        {
            Certificate = certificate;
        }

        public override XmlElement GetXml()
        {
            return GetXml(new XmlDocument() { PreserveWhitespace = true });
        }

        private XmlElement GetXml(XmlDocument xmlDocument)
        {
            XmlElement element1 = xmlDocument.CreateElement("wsse", "SecurityTokenReference", Navnerom.wsse);

            var idAttribute = xmlDocument.CreateAttribute("wsu", "Id", Navnerom.wsu);
            idAttribute.Value = "STR-0C65FAC3DD4C8DBAAD142827305718654";
            element1.SetAttributeNode(idAttribute);

            if (!string.IsNullOrEmpty(Uri))
            {
                XmlElement element2 = xmlDocument.CreateElement("Reference", Navnerom.wsse);
                element2.SetAttribute("URI", Uri);
                element2.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
                element1.AppendChild(element2);
            }
            if (Certificate != null)
            {
                var keyIdentifier  = xmlDocument.CreateElement("wsse", "KeyIdentifier", Navnerom.wsse);
                keyIdentifier.SetAttribute("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
                keyIdentifier.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
                keyIdentifier.InnerText = Convert.ToBase64String(Certificate.RawData); 
                element1.AppendChild(keyIdentifier);
            }

            return element1;
        }

        public override void LoadXml(XmlElement element)
        {
        }
    }
}
