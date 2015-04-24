using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private readonly string _rotressurssti;
        private readonly Dictionary<string, string> _ressurserEtterNavnerom = new Dictionary<string, string>();
        

        /// <summary>
        /// Rotsti til hvor ressursene ligger i prosjektet. Dette er en sti hvor stiseparator er punktum (.).
        /// Hvis alle XSD-filer liggger i mappe Mitt.Prosjektnavn/Xsd/Minxsd.xsd, så blir ressurssti 
        /// Mitt.Prosjektnavn.Xsd
        /// </summary>
        /// <param name="rotressurssti"></param>
        protected XmlValidator(string rotressurssti)
        {
            _rotressurssti = rotressurssti;
        }

        public string ValideringsVarsler { get; private set; }

        private XmlSchemaSet GenererSchemaSet()
        {
            if (_ressurserEtterNavnerom.Count == 0)
            {
                throw new IndexOutOfRangeException("Ingen xsdressurser funnet. Legg til ved å bruke LeggTilRessurs()");
            }

            var schemaSet = new XmlSchemaSet();
            foreach (KeyValuePair<string,string> par in _ressurserEtterNavnerom )
            {
                schemaSet.Add(par.Key, XsdResource(par.Value));
            }
            return schemaSet;
        }

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

       /// <summary>
        /// Legg til en referanse til en XSD-fil.
        /// </summary>
        /// <param name="navnerom">Navnerom på xmlfil som lastes inn.</param>
        /// <param name="dottstiTilRessurs">Dottede stien til en ressurs relativt til rotressurssti som klassen ble initialisert med. </param>
        protected void LeggTilRessurs(string navnerom, string dottstiTilRessurs)
        {
            _ressurserEtterNavnerom.Add(navnerom,dottstiTilRessurs);
        }

        private XmlReader XsdResource(string resource)
        {
            ResourceUtility resourceUtility = new ResourceUtility(_rotressurssti);
            Stream s = new MemoryStream(resourceUtility.ReadAllBytes(true, resource));
            return XmlReader.Create(s);
        }
    }
}
