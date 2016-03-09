using System.IO;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class ResponseDokument
    {
        public XmlNamespaceManager Nsmgr;

        public ResponseDokument(Stream soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(soapResponse);
            PopulateXmlElements(xmlDocument);
        }

        public ResponseDokument(XmlDocument xmlEnvelope)
        {
            PopulateXmlElements(xmlEnvelope);
        }

        public XmlNode BodyElement { get; set; }

        public XmlNode TimestampElement { get; set; }

        public string Cipher { get; set; }

        public XmlElement EncryptedBody { get; set; }

        public XmlDocument Envelope { get; set; }

        public XmlElement HeaderSecurityElement { get; set; }

        public XmlElement HeaderSignatureElement { get; set; }

        public XmlElement HeaderSignature { get; private set; }

        public XmlElement HeaderBinarySecurityToken { get; set; }

        private void PopulateXmlElements(XmlDocument xmlEnvelope)
        {
            Nsmgr = NamespaceManager.InitalizeNamespaceManager(xmlEnvelope);
            Envelope = xmlEnvelope;

            const string headerWsseSecurity = "/env:Envelope/env:Header/wsse:Security";
            const string dsSignature = "./ds:Signature";
            const string dsSignaturevalue = "./ds:SignatureValue";
            const string bodyXencEncrypteddata = "/env:Envelope/env:Body/xenc:EncryptedData";
            const string wsseBinarysecuritytoken = "./wsse:BinarySecurityToken";
            const string ciphervalue = "/env:Envelope/env:Header/wsse:Security/xenc:EncryptedKey/xenc:CipherData/xenc:CipherValue";
            const string wsuTimestamp = "./wsu:Timestamp";
            const string body = "/env:Envelope/env:Body";

            HeaderSecurityElement = Envelope.SelectSingleNode(headerWsseSecurity, Nsmgr) as XmlElement;
            HeaderSignatureElement = HeaderSecurityElement.SelectSingleNode(dsSignature, Nsmgr) as XmlElement;
            HeaderSignature = HeaderSignatureElement.SelectSingleNode(dsSignaturevalue, Nsmgr) as XmlElement;
            EncryptedBody = Envelope.SelectSingleNode(bodyXencEncrypteddata, Nsmgr) as XmlElement;
            HeaderBinarySecurityToken =
                HeaderSecurityElement.SelectSingleNode(wsseBinarysecuritytoken, Nsmgr) as XmlElement;
            Cipher = Envelope.SelectSingleNode(ciphervalue, Nsmgr).InnerText;
            TimestampElement = HeaderSecurityElement.SelectSingleNode(wsuTimestamp, Nsmgr);

            BodyElement = Envelope.SelectSingleNode(body, Nsmgr);
        }
    }
}