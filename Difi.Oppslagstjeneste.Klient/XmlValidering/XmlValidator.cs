using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.XmlValidering
{
    internal abstract class XmlValidator
    {
        private bool _harWarnings;
        private bool _harErrors;

        private const string ErrorToleratedPrefix = "The 'PrefixList' attribute is invalid - The value '' is invalid according to its datatype 'http://www.w3.org/2001/XMLSchema:NMTOKENS' - The attribute value cannot be empty.";


        private const string WarningMessage = "\tWarning: Matching schema not found. No validation occurred.";
        private const string ErrorMessage = "\tValidation error:";

        public string ValideringsVarsler { get; private set; }

        protected abstract XmlSchemaSet GenererSchemaSet();

        public bool ValiderDokumentMotXsd(string document)
        {
            var settings = new XmlReaderSettings();
            settings.Schemas.Add(GenererSchemaSet());
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += ValidationEventHandler;

            var xmlReader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(document)), settings);

            while (xmlReader.Read()) { }

            return _harErrors == false && _harWarnings == false;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Warning:
                    ValideringsVarsler += String.Format("{0} {1}\n", WarningMessage, e.Message);
                    _harWarnings = true;
                    break;
                case XmlSeverityType.Error:
                    ValideringsVarsler += String.Format("{0} {1}\n", ErrorMessage, e.Message);
                    if (!e.Message.Equals(ErrorToleratedPrefix))
                        _harErrors = true;
                    else
                        ValideringsVarsler +=
                            "Feilen over er ikke noe vi håndterer, og er heller ikke årsaken til at validering feilet\n";
                    break;
            }
        }

        protected XmlReader XsdFromResource(XsdRessurs xsdRessurs)
        {
            return XsdResource(_xsdRessurserDictionary[xsdRessurs]);
        }

        private XmlReader XsdResource(string resource)
        {
            ResourceUtility resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.XmlValidering.Xsd");
            Stream s = new MemoryStream(resourceUtility.ReadAllBytes(true, resource));
            return XmlReader.Create(s);
        }

        public enum XsdRessurs
        {
            OppslagstjenesteDefinisjon,
            OppslagstjenesteMetadata,
            WssecuritySecext10,
            WssecurityUtility10,
            SoapEnvelope,
            XmlDsig,
            XmlExcC14
        }

        private readonly Dictionary<XsdRessurs, string> _xsdRessurserDictionary = new Dictionary<XsdRessurs, string>
        {
            {XsdRessurs.OppslagstjenesteDefinisjon, "oppslagstjeneste-ws-14-05.xsd" },
            {XsdRessurs.OppslagstjenesteMetadata, "oppslagstjeneste-metadata-14-05.xsd"},
            {XsdRessurs.WssecuritySecext10, "wssecurity.oasis-200401-wss-wssecurity-secext-1.0.xsd"},
            {XsdRessurs.WssecurityUtility10, "wssecurity.oasis-200401-wss-wssecurity-utility-1.0.xsd"},
            {XsdRessurs.SoapEnvelope, "soap.soap.xsd"},
            {XsdRessurs.XmlDsig, "w3.xmldsig-core-schema.xsd"},
            {XsdRessurs.XmlExcC14, "w3.exc-c14n.xsd"}
        };
    }
}
