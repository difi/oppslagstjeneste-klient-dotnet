using System.IO;
using System.Xml;
using ApiClientShared;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.XmlValidation
{
    internal class OppslagstjenesteXmlValidator : XmlValidator
    {
        private static readonly ResourceUtility ResourceUtility =
            new ResourceUtility("Difi.Oppslagstjeneste.Klient.XmlValidation.Xsd");

        public OppslagstjenesteXmlValidator()
        {
            AddXsd(Navnerom.OppslagstjenesteDefinisjon, HentRessurs("oppslagstjeneste-ws-16-02.xsd"));
            AddXsd(Navnerom.OppslagstjenesteMetadata, HentRessurs("oppslagstjeneste-metadata-16-02.xsd"));
            AddXsd(Navnerom.WssecuritySecext10, HentRessurs("wssecurity.oasis-200401-wss-wssecurity-secext-1.0.xsd"));
            AddXsd(Navnerom.WssecurityUtility10, HentRessurs("wssecurity.oasis-200401-wss-wssecurity-utility-1.0.xsd"));
            AddXsd(Navnerom.SoapEnvelope12, HentRessurs("soap.soap.xsd"));
            AddXsd(Navnerom.XmlDsig, HentRessurs("w3.xmldsig-core-schema.xsd"));
            AddXsd(Navnerom.XmlExcC14N, HentRessurs("w3.exc-c14n.xsd"));
            AddXsd(Navnerom.XmlNameSpace, HentRessurs("w3.xml.xsd"));
        }

        private static XmlReader HentRessurs(string path)
        {
            var bytes = ResourceUtility.ReadAllBytes(true, path);
            return XmlReader.Create(new MemoryStream(bytes));
        }
    }
}