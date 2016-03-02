using System;
using System.Xml;
using Difi.Felles.Utility.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SoapException : DifiException
    {
        public SoapException(string outerXml)
            : this(outerXml, null)
        {
        }

        public SoapException(string outerXml, Exception innerException)
            : base("SoapException: Klarte ikke parse svar fra serveren.", innerException)
        {
            ParseTilKlassemedlemmer(outerXml);
        }

        public string Xml { get; set; }

        public string Skyldig { get; set; }

        public string Beskrivelse { get; set; }

        private void ParseTilKlassemedlemmer(string outerXml)
        {
            Xml = outerXml;

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(outerXml);

                var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                namespaceManager.AddNamespace("SOAP-ENV", Navnerom.SoapEnvelope12);

                var rot = xmlDocument.DocumentElement;
                Skyldig = rot.SelectSingleNode("//faultcode", namespaceManager).InnerText;
                Beskrivelse = rot.SelectSingleNode("//faultstring", namespaceManager).InnerText;
            }
            catch (Exception e)
            {
                throw new XmlParseException(
                    "Feilmelding mottatt, klarte ikke å parse feilkode og feilmelding. Se Xml for rådata.", e);
            }
        }
    }
}