using System.Xml;
using System.Xml.Schema;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.XmlValidering
{
    internal class OppslagstjenesteXmlvalidator : XmlValidator
    {
        public OppslagstjenesteXmlvalidator(string rotressurssti) : base(rotressurssti)
        {
            LeggTilRessurs(Navnerom.OppslagstjenesteDefinisjon, "oppslagstjeneste-ws-14-05.xsd");
            LeggTilRessurs(Navnerom.OppslagstjenesteMetadata, "oppslagstjeneste-metadata-14-05.xsd");
            LeggTilRessurs(Navnerom.WssecuritySecext10, "wssecurity.oasis-200401-wss-wssecurity-secext-1.0.xsd");
            LeggTilRessurs(Navnerom.WssecurityUtility10, "wssecurity.oasis-200401-wss-wssecurity-utility-1.0.xsd");
            LeggTilRessurs(Navnerom.SoapEnvelope, "soap.soap.xsd");
            LeggTilRessurs(Navnerom.XmlDsig, "w3.xmldsig-core-schema.xsd");
            LeggTilRessurs(Navnerom.XmlExcC14n, "w3.exc-c14n.xsd");
        }
    }
}
