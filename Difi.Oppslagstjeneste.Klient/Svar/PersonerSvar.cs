using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    public class PersonerSvar : Svar
    {
        public PersonerSvar()
        {
        }

        public PersonerSvar(XmlDocument xmlDocument) : base(xmlDocument)
        {
        }

        public IEnumerable<Person> Personer { get; set; }

        protected override void ParseTilKlassemedlemmer()
        {
            var bodyElement = XmlDocument.SelectSingleNode("/env:Envelope/env:Body", XmlNamespaceManager);
            var deserialisetPersonSvar = SerializeUtil.Deserialize<DTO.HentPersonerRespons>(bodyElement.InnerXml);
            Personer = DtoKonverterer.TilDomeneObjekt(deserialisetPersonSvar).Personer;
        }
    }
}