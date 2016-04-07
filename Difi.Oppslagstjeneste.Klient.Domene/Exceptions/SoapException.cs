﻿using System;
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
            : base("Sjekk klassemedlemmer for mer detaljer.", innerException)
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
                namespaceManager.AddNamespace("env", Navnerom.SoapEnvelope12);

                var rot = xmlDocument.DocumentElement;
                Skyldig = rot.SelectSingleNode("./env:Body/env:Fault/env:Code/env:Value", namespaceManager).InnerText;
                Beskrivelse = rot.SelectSingleNode("./env:Body/env:Fault/env:Reason/env:Text", namespaceManager).InnerText;
            }
            catch (Exception e)
            {
                throw new XmlParseException(
                    "Feilmelding mottatt, klarte ikke å parse feilkode og feilmelding. Se Xml for rådata.", e);
            }
        }
    }
}