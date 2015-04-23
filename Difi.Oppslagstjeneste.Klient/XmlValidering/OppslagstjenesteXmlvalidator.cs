using System.Xml.Schema;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.XmlValidering
{
    internal class OppslagstjenesteXmlvalidator : XmlValidator
    {
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
