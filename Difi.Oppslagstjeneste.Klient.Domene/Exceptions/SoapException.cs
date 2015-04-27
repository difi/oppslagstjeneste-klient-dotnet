using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SoapException : OppslagstjenesteException
    {
        public SoapException(string outerXml)
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
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(outerXml);

                XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                namespaceManager.AddNamespace("SOAP-ENV", Navnerom.SoapEnvelope);

                var rot = xmlDocument.DocumentElement;
                Skyldig = rot.SelectSingleNode("//faultcode", namespaceManager).InnerText;
                Beskrivelse = rot.SelectSingleNode("//faultstring", namespaceManager).InnerText;
            }
            catch ( Exception e)
            {
                throw new XmlParseException("Feilmelding mottat, klarte ikke å parse feilkode og feilmelding. Se Xml for rådata.", e);
            }
        }
    }
}
