using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Epostadresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Epostadresse;
using Kontaktinformasjon = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Kontaktinformasjon;
using Mobiltelefonnummer = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Mobiltelefonnummer;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;
using SikkerDigitalPostAdresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.SikkerDigitalPostAdresse;

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
                Personidentifikator = _person.personidentifikator
            };

            if (_person.reservasjonSpecified)
                person.Reservasjon = TilDomeneObjekt(_person.reservasjon).Value;
            person.SikkerDigitalPostAdresse = TilDomeneObjekt(_person.SikkerDigitalPostAdresse);
            if (_person.statusSpecified)
                person.Status = TilDomeneObjekt(_person.status);
            person.X509Sertifikat = TilDomeneObjekt(_person.X509Sertifikat);
            if (_person.varslingsstatusSpecified)
                person.Varslingsstatus = (Varslingsstatus) Enum.Parse(typeof (Varslingsstatus), _person.varslingsstatus.ToString());

            return person;
        }

        public static X509Certificate2 TilDomeneObjekt(byte[] x509Sertifikat)
        {
            return x509Sertifikat == null ? null : new X509Certificate2(x509Sertifikat);
        }

        private static Status TilDomeneObjekt(status status)
        {
            var tilstand = Enum.Parse(typeof (Status), status.ToString());
            return (Status) tilstand;
        }

        private static SikkerDigitalPostAdresse TilDomeneObjekt(DTO.SikkerDigitalPostAdresse _sikkerDigitalPostAdresse)
        {
            if (_sikkerDigitalPostAdresse == null)
            {
                return null;
            }

            var sikkerDigitalPostAdresse = new SikkerDigitalPostAdresse
            {
                Postkasseadresse = _sikkerDigitalPostAdresse.postkasseadresse,
                PostkasseleverandørAdresse = _sikkerDigitalPostAdresse.postkasseleverandoerAdresse
            };
            return sikkerDigitalPostAdresse;
        }

        private static bool? TilDomeneObjekt(reservasjon reservasjon)
        {
            return reservasjon == reservasjon.JA;
        }

        private static Kontaktinformasjon TilDomeneObjekt(DTO.Kontaktinformasjon _kontaktinformasjon)
        {
            if (_kontaktinformasjon == null)
                return null;

            var kontaktinformasjon = new Kontaktinformasjon
            {
                Epostadresse = TilDomeneObjekt(_kontaktinformasjon.Epostadresse),
                Mobiltelefonnummer = TilDomeneObjekt(_kontaktinformasjon.Mobiltelefonnummer)
            };

            return kontaktinformasjon;
        }

        private static Mobiltelefonnummer TilDomeneObjekt(DTO.Mobiltelefonnummer _mobiltelefonnummer)
        {
            if (_mobiltelefonnummer == null)
                return null;

            var mobiltelefonnummer = new Mobiltelefonnummer {Nummer = _mobiltelefonnummer.Value};
            if (_mobiltelefonnummer.sistOppdatertSpecified)
            {
                mobiltelefonnummer.SistOppdatert = _mobiltelefonnummer.sistOppdatert;
            }
            if (_mobiltelefonnummer.sistVerifisertSpecified)
            {
                mobiltelefonnummer.SistVerifisert = _mobiltelefonnummer.sistVerifisert;
            }

            return mobiltelefonnummer;
        }

        internal static EndringerSvar TilDomeneObjekt(HentEndringerRespons hentEndringerRespons)
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
            if (_epostadresse == null)
                return null;

            var epostadresse = new Epostadresse {Epost = _epostadresse.Value};
            if (_epostadresse.sistOppdatertSpecified)
            {
                epostadresse.SistOppdatert = _epostadresse.sistOppdatert;
            }
            if (_epostadresse.sistVerifisertSpecified)
            {
                epostadresse.SistVerifisert = _epostadresse.sistVerifisert;
            }

            return epostadresse;
        }

        internal static PersonerSvar TilDomeneObjekt(HentPersonerRespons _hentPersonerRespons)
        {
            var personerSvar = new PersonerSvar {Personer = TilDomeneObjekt(_hentPersonerRespons.Person)};

            return personerSvar;
        }
    }
}