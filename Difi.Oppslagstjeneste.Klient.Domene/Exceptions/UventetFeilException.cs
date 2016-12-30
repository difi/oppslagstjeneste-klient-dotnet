using System;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class UventetFeilException : OppslagstjenesteException
    {
        public UventetFeilException(XmlDocument outerXml)
            : this(outerXml, null)
        {
        }

        public UventetFeilException(XmlDocument xml, Exception innerException)
            : base("Sjekk klassemedlemmer for mer detaljer.", innerException)
        {
            ParseToClassMembers(xml);
        }

        public XmlDocument Xml { get; set; }

        public string Skyldig { get; set; }

        public string Beskrivelse { get; set; }

        private void ParseToClassMembers(XmlDocument outerXml)
        {
            Xml = outerXml;

            try
            {
                var namespaceManager = new XmlNamespaceManager(outerXml.NameTable);
                namespaceManager.AddNamespace("env", Navnerom.SoapEnvelope12);

                var rot = outerXml.DocumentElement;
                Skyldig = rot.SelectSingleNode("./env:Body/env:Fault/env:Code/env:Value", namespaceManager).InnerText;
                Beskrivelse = rot.SelectSingleNode("./env:Body/env:Fault/env:Reason/env:Text", namespaceManager).InnerText;
            }
            catch (Exception e)
            {
                throw new XmlParseException(
                    "Feilmelding mottatt, klarte ikke å parse feilkode og feilmelding. Se Xml for rådata.", e);
            }
        }

        public override string ToString()
        {
            return $"Skyldig: {Skyldig}, Beskrivelse: {Beskrivelse}";
        }
    }
}