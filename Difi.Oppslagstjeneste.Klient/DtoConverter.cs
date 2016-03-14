using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Epostadresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Epostadresse;
using Kontaktinformasjon = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Kontaktinformasjon;
using Mobiltelefonnummer = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Mobiltelefonnummer;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;
using SikkerDigitalPostAdresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.SikkerDigitalPostAdresse;

namespace Difi.Oppslagstjeneste.Klient
{
    public class DtoConverter
    {
        public static IEnumerable<Person> ToDomainObject(DTO.Person[] person)
        {
            return person.Select(ToDomainObject).ToList();
        }

        public static Person ToDomainObject(DTO.Person dtoPerson)
        {
            var person = new Person
            {
                Kontaktinformasjon = ToDomainObject(dtoPerson.Kontaktinformasjon),
                Personidentifikator = dtoPerson.personidentifikator
            };

            if (dtoPerson.reservasjonSpecified)
                person.Reservasjon = ToDomainObject(dtoPerson.reservasjon).Value;
            person.SikkerDigitalPostAdresse = ToDomainObject(dtoPerson.SikkerDigitalPostAdresse);
            if (dtoPerson.statusSpecified)
                person.Status = ToDomainObject(dtoPerson.status);
            person.X509Sertifikat = ToDomainObject(dtoPerson.X509Sertifikat);
            if (dtoPerson.varslingsstatusSpecified)
                person.Varslingsstatus = (Varslingsstatus) Enum.Parse(typeof (Varslingsstatus), dtoPerson.varslingsstatus.ToString());

            return person;
        }

        public static X509Certificate2 ToDomainObject(byte[] x509Sertifikat)
        {
            return x509Sertifikat == null ? null : new X509Certificate2(x509Sertifikat);
        }

        private static Status ToDomainObject(status status)
        {
            var tilstand = Enum.Parse(typeof (Status), status.ToString());
            return (Status) tilstand;
        }

        private static SikkerDigitalPostAdresse ToDomainObject(DTO.SikkerDigitalPostAdresse dtoSikkerDigitalPostAdresse)
        {
            if (dtoSikkerDigitalPostAdresse == null)
            {
                return null;
            }

            var sikkerDigitalPostAdresse = new SikkerDigitalPostAdresse
            {
                Postkasseadresse = dtoSikkerDigitalPostAdresse.postkasseadresse,
                PostkasseleverandørAdresse = dtoSikkerDigitalPostAdresse.postkasseleverandoerAdresse
            };
            return sikkerDigitalPostAdresse;
        }

        private static bool? ToDomainObject(reservasjon reservasjon)
        {
            return reservasjon == reservasjon.JA;
        }

        private static Kontaktinformasjon ToDomainObject(DTO.Kontaktinformasjon dtoKontaktinformasjon)
        {
            if (dtoKontaktinformasjon == null)
                return null;

            var kontaktinformasjon = new Kontaktinformasjon
            {
                Epostadresse = ToDomainObject(dtoKontaktinformasjon.Epostadresse),
                Mobiltelefonnummer = ToDomainObject(dtoKontaktinformasjon.Mobiltelefonnummer)
            };

            return kontaktinformasjon;
        }

        private static Mobiltelefonnummer ToDomainObject(DTO.Mobiltelefonnummer dtoMobiltelefonnummer)
        {
            if (dtoMobiltelefonnummer == null)
                return null;

            var mobiltelefonnummer = new Mobiltelefonnummer {Nummer = dtoMobiltelefonnummer.Value};
            if (dtoMobiltelefonnummer.sistOppdatertSpecified)
            {
                mobiltelefonnummer.SistOppdatert = dtoMobiltelefonnummer.sistOppdatert;
            }
            if (dtoMobiltelefonnummer.sistVerifisertSpecified)
            {
                mobiltelefonnummer.SistVerifisert = dtoMobiltelefonnummer.sistVerifisert;
            }

            return mobiltelefonnummer;
        }

        internal static EndringerSvar ToDomainObject(HentEndringerRespons hentEndringerRespons)
        {
            var endringerSvar = new EndringerSvar
            {
                Personer = ToDomainObject(hentEndringerRespons.Person),
                FraEndringsNummer = hentEndringerRespons.fraEndringsNummer,
                TilEndringsNummer = hentEndringerRespons.tilEndringsNummer,
                SenesteEndringsNummer = hentEndringerRespons.senesteEndringsNummer
            };

            return endringerSvar;
        }

        private static Epostadresse ToDomainObject(DTO.Epostadresse dtoEpostadresse)
        {
            if (dtoEpostadresse == null)
                return null;

            var epostadresse = new Epostadresse {Epost = dtoEpostadresse.Value};
            if (dtoEpostadresse.sistOppdatertSpecified)
            {
                epostadresse.SistOppdatert = dtoEpostadresse.sistOppdatert;
            }
            if (dtoEpostadresse.sistVerifisertSpecified)
            {
                epostadresse.SistVerifisert = dtoEpostadresse.sistVerifisert;
            }

            return epostadresse;
        }

        internal static PersonerSvar ToDomainObject(HentPersonerRespons dtoHentPersonerRespons)
        {
            var personerSvar = new PersonerSvar {Personer = ToDomainObject(dtoHentPersonerRespons.Person)};

            return personerSvar;
        }

        internal static PrintSertifikatSvar ToDomainObject(HentPrintSertifikatRespons result)
        {
            var printSertifikatSvar = new PrintSertifikatSvar
            {
                Printsertifikat = ToDomainObject(result.X509Sertifikat),
                PostkasseleverandørAdresse = result.postkasseleverandoerAdresse
            };

            return printSertifikatSvar;
        }
        
    }
}