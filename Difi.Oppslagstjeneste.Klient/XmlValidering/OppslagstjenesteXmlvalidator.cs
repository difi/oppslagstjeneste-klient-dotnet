using System.Xml;
using System.Xml.Schema;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.XmlValidering
{
    internal class OppslagstjenesteXmlvalidator : XmlValidator
    {
        public OppslagstjenesteXmlvalidator(string rotressurssti) : base(rotressurssti)
        {
            LeggTilRessurs(XsdRessurs.OppslagstjenesteDefinisjon, "oppslagstjeneste-ws-14-05.xsd");
            LeggTilRessurs(XsdRessurs.OppslagstjenesteMetadata, "oppslagstjeneste-metadata-14-05.xsd");
            LeggTilRessurs(XsdRessurs.WssecuritySecext10, "wssecurity.oasis-200401-wss-wssecurity-secext-1.0.xsd");
            LeggTilRessurs(XsdRessurs.WssecurityUtility10, "wssecurity.oasis-200401-wss-wssecurity-utility-1.0.xsd");
            LeggTilRessurs(XsdRessurs.SoapEnvelope, "soap.soap.xsd");
            LeggTilRessurs(XsdRessurs.XmlDsig, "w3.xmldsig-core-schema.xsd");
            LeggTilRessurs(XsdRessurs.XmlExcC14, "w3.exc-c14n.xsd");
        }

        protected override XmlSchemaSet GenererSchemaSet()
        {
            var schemaSet = new XmlSchemaSet();
            schemaSet.Add(Navnerom.krr, XsdFromResource(XsdRessurs.OppslagstjenesteDefinisjon));
            schemaSet.Add(Navnerom.difi, XsdFromResource(XsdRessurs.OppslagstjenesteMetadata));
            schemaSet.Add(Navnerom.wsse, XsdFromResource(XsdRessurs.WssecuritySecext10));
            schemaSet.Add(Navnerom.wsu, XsdFromResource(XsdRessurs.WssecurityUtility10));
            schemaSet.Add(Navnerom.Ns2, XsdFromResource(XsdRessurs.SoapEnvelope));
            schemaSet.Add(Navnerom.ds, XsdFromResource(XsdRessurs.XmlDsig));
            schemaSet.Add(Navnerom.ec, XsdFromResource(XsdRessurs.XmlExcC14));
            return schemaSet;
        }
    }
}
