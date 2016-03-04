using System.Collections.Generic;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.DTO;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class PersonerSvar : Svar
    {
        public PersonerSvar()
        {
        }

        public PersonerSvar(XmlDocument xmlDocument)
            : base(xmlDocument)
        {
        }

        public IEnumerable<Person> Personer { get; set; }

        protected override void ParseTilKlassemedlemmer()
        {
            var bodyElement = XmlDocument.SelectSingleNode("/env:Envelope/env:Body", XmlNamespaceManager);
            var deserialisetPersonSvar = SerializeUtil.Deserialize<HentPersonerRespons>(bodyElement.InnerXml);
            Personer = DtoKonverterer.TilDomeneObjekt(deserialisetPersonSvar).Personer;
        }
    }
}