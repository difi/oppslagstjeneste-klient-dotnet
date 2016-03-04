using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;

using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient
{
    public class DtoKonverterer
    {
        public static IEnumerable<Person> TilDomeneObjekt(DTO.Person[] person)
        {
            return person.Select(TilDomeneObjekt).ToList();
        }

        public static Person TilDomeneObjekt(DTO.Person _person)
        {
            var person = new Person
            {
                Kontaktinformasjon = TilDomeneObjekt(_person.Kontaktinformasjon),
                Personidentifikator = _person.personidentifikator,
                Reservasjon = TilDomeneObjekt(_person.reservasjon),
                SikkerDigitalPostAdresse = TilDomeneObjekt(_person.SikkerDigitalPostAdresse),
                Status = TilDomeneObjekt(_person.status),
                X509Sertifikat = TilDomeneObjekt(_person.X509Sertifikat)
            };

            return person;
        }

        private static X509Certificate2 TilDomeneObjekt(byte[] x509Sertifikat)
        {
            return x509Sertifikat == null ? null : new X509Certificate2(x509Sertifikat);
        }

        private static Status TilDomeneObjekt(DTO.status status)
        {
            var tilstand = Enum.Parse(typeof(Status),status.ToString());
            return (Status) tilstand;
        }

        private static SikkerDigitalPostAdresse TilDomeneObjekt(DTO.SikkerDigitalPostAdresse _sikkerDigitalPostAdresse)
        {
            var sikkerDigitalPostAdresse = new SikkerDigitalPostAdresse
            {
                Postkasseadresse = _sikkerDigitalPostAdresse.postkasseadresse,
                PostkasseleverandørAdresse = _sikkerDigitalPostAdresse.postkasseleverandoerAdresse
            };
            return sikkerDigitalPostAdresse;
        }

        private static bool? TilDomeneObjekt(DTO.reservasjon reservasjon)
        {
            return reservasjon == DTO.reservasjon.JA;
        }

        private static Kontaktinformasjon TilDomeneObjekt(DTO.Kontaktinformasjon _kontaktinformasjon)
        {
            var kontaktinformasjon = new Kontaktinformasjon();
            kontaktinformasjon.Epostadresse = TilDomeneObjekt(_kontaktinformasjon.Epostadresse);
            kontaktinformasjon.Mobiltelefonnummer = TilDomeneObjekt(_kontaktinformasjon.Mobiltelefonnummer);

            return kontaktinformasjon;
        }

        private static Mobiltelefonnummer TilDomeneObjekt(DTO.Mobiltelefonnummer _mobiltelefonnummer)
        {
            var mobiltelefonnummer = new Mobiltelefonnummer
            {
                Nummer = _mobiltelefonnummer.Value,
                SistOppdatert = _mobiltelefonnummer.sistOppdatert,
                SistVerifisert = _mobiltelefonnummer.sistVerifisert
            };

            return mobiltelefonnummer;
        }

        internal static EndringerSvar TilDomeneObjekt(DTO.HentEndringerRespons hentEndringerRespons)
        {
            var endringerSvar = new EndringerSvar
            {
                Personer = TilDomeneObjekt(hentEndringerRespons.Person),
                FraEndringsNummer = hentEndringerRespons.fraEndringsNummer,
                TilEndringsNummer = hentEndringerRespons.tilEndringsNummer,
                SenesteEndringsNummer = hentEndringerRespons.senesteEndringsNummer
            };

            return endringerSvar;
        }

        private static Epostadresse TilDomeneObjekt(DTO.Epostadresse _epostadresse)
        {
            var epostadresse = new Epostadresse
            {
                Epost = _epostadresse.Value,
                SistOppdatert = _epostadresse.sistOppdatert,
                SistVerifisert = _epostadresse.sistVerifisert
            };

            return epostadresse;
        }

        internal static PersonerSvar TilDomeneObjekt(DTO.HentPersonerRespons _hentPersonerRespons)
        {
            var personerSvar = new PersonerSvar {Personer = TilDomeneObjekt(_hentPersonerRespons.Person)};

            return personerSvar;
        }

    }
}
