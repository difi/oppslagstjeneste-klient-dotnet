using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class HentPersonerSvar : HentSvar
    {
        public HentPersonerSvar(XmlDocument xmlDocument) : base(xmlDocument)
        {
            ParseToClassMembers(xmlDocument);
        }

        private void ParseToClassMembers(XmlDocument xmlDocument)
        {
            try
            {
                var personElements = xmlDocument.SelectNodes(
                    "/env:Envelope/env:Body/ns:HentPersonerRespons/difi:Person", XmlNamespaceManager);
                List<Person> result =
                    (from object item in personElements select new Person(item as XmlElement)).ToList();

                Personer = result;
            }
            catch (Exception e)
            {
                throw new XmlParseException("Klarte ikke å parse svar fra Oppslagstjenesten.", e);
            }
        }

        public IEnumerable<Person> Personer { get; set; }
    }
}
