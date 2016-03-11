using System.IO;
using System.Xml;
using ApiClientShared;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.XmlValidering
{
    internal class OppslagstjenesteXmlvalidator : XmlValidator
    {
        private static readonly ResourceUtility ResourceUtility =
            new ResourceUtility("Difi.Oppslagstjeneste.Klient.XmlValidering.Xsd");

        public OppslagstjenesteXmlvalidator()
        {
            LeggTilXsdRessurs(Navnerom.OppslagstjenesteDefinisjon, HentRessurs("oppslagstjeneste-ws-16-02.xsd"));
            LeggTilXsdRessurs(Navnerom.OppslagstjenesteMetadata, HentRessurs("oppslagstjeneste-metadata-16-02.xsd"));
            LeggTilXsdRessurs(Navnerom.WssecuritySecext10,
                HentRessurs("wssecurity.oasis-200401-wss-wssecurity-secext-1.0.xsd"));
            LeggTilXsdRessurs(Navnerom.WssecurityUtility10,
                HentRessurs("wssecurity.oasis-200401-wss-wssecurity-utility-1.0.xsd"));
            LeggTilXsdRessurs(Navnerom.SoapEnvelope12, HentRessurs("soap.soap.xsd"));
            LeggTilXsdRessurs(Navnerom.XmlDsig, HentRessurs("w3.xmldsig-core-schema.xsd"));
            LeggTilXsdRessurs(Navnerom.XmlExcC14N, HentRessurs("w3.exc-c14n.xsd"));


        }

        private XmlReader HentRessurs(string path)
        {
            var bytes = ResourceUtility.ReadAllBytes(true, path);
            return XmlReader.Create(new MemoryStream(bytes));
        }
    }
}