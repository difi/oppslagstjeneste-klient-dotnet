using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class PersonerSvar : Svar
    {
        public PersonerSvar(XmlDocument xmlDocument)
            : base(xmlDocument)
        {
        }

        public IEnumerable<Person> Personer { get; set; }

        protected override void ParseTilKlassemedlemmer()
        {
            try
            {
                var personElements = XmlDocument.SelectNodes(
                    "/env:Envelope/env:Body/ns:HentPersonerRespons/difi:Person", XmlNamespaceManager);
                var result =
                    (from object item in personElements select new Person(item as XmlElement)).ToList();

                Personer = result;
            }
            catch (Exception e)
            {
                throw new XmlParseException("Klarte ikke å parse svar fra Oppslagstjenesten.", e);
            }
        }
    }
}