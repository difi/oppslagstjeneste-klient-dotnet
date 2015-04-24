using System.Collections.Generic;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient
{
    public class HentPersonerSvar
    {
        private XmlDocument document;

        public HentPersonerSvar(XmlDocument xmlDocument)
        {
            document = xmlDocument;
        }

        public IEnumerable<Person> Personer()
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("env", Navnerom.SoapEnvelope);
            nsmgr.AddNamespace("ns", Navnerom.OppslagstjenesteDefinisjon);
            nsmgr.AddNamespace("difi", Navnerom.OppslagstjenesteMetadata); 
            
            var personElements = document.SelectNodes("/env:Envelope/env:Body/ns:HentPersonerRespons/difi:Person", nsmgr);

            List<Person> result = new List<Person>();
            foreach (var item in personElements)
            {
                result.Add(new Person(item as XmlElement));
            }
            
            return result;
        }
    }
}
