using System.IO;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class ResponseDokument
    {
        public XmlNamespaceManager Nsmgr;

        public ResponseDokument(Stream soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(soapResponse);
            ByggOppStruktur(xmlDocument);
        }

        public ResponseDokument(XmlDocument xmlEnvelope)
        {
            ByggOppStruktur(xmlEnvelope);
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
        
        private void ByggOppStruktur(XmlDocument xmlEnvelope)
        {
            Nsmgr = NamespaceManager.InitalizeNamespaceManager(xmlEnvelope);
            Envelope = xmlEnvelope;

            HeaderSecurityElement =
                Envelope.SelectSingleNode("/env:Envelope/env:Header/wsse:Security", Nsmgr) as XmlElement;
            HeaderSignatureElement = HeaderSecurityElement.SelectSingleNode("./ds:Signature", Nsmgr) as XmlElement;
            HeaderSignature = HeaderSignatureElement.SelectSingleNode("./ds:SignatureValue", Nsmgr) as XmlElement;
            EncryptedBody = Envelope.SelectSingleNode("/env:Envelope/env:Body/xenc:EncryptedData", Nsmgr) as XmlElement;
            HeaderBinarySecurityToken =
                HeaderSecurityElement.SelectSingleNode("./wsse:BinarySecurityToken", Nsmgr) as XmlElement;
            Cipher = Envelope.SelectSingleNode(
                "/env:Envelope/env:Header/wsse:Security/xenc:EncryptedKey/xenc:CipherData/xenc:CipherValue", Nsmgr)
                .InnerText;
            TimestampElement = HeaderSecurityElement.SelectSingleNode("./wsu:Timestamp", Nsmgr);
            
            BodyElement = Envelope.SelectSingleNode("/env:Envelope/env:Body", Nsmgr);
        }

        public T ToDtoObject<T>()
        {
            var deserializedResponse = SerializeUtil.Deserialize<T>(BodyElement.InnerXml);
            return deserializedResponse;
        }
    }
}