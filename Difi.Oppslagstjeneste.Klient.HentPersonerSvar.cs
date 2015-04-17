using System.Collections.Generic;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;
using Difi.Oppslagstjenesten.Domene;

namespace Difi.Oppslagstjenesten
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
            nsmgr.AddNamespace("env", Navnerom.env11);
            nsmgr.AddNamespace("ns", Navnerom.krr);
            nsmgr.AddNamespace("difi", Navnerom.difi); 
            
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
