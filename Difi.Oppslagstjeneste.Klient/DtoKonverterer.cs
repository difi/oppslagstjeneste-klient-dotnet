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

        public static Person TilDomeneObjekt(DTO.Person dtoPerson)
        {
            var person = new Person
            {
                Kontaktinformasjon = TilDomeneObjekt(dtoPerson.Kontaktinformasjon),
                Personidentifikator = dtoPerson.personidentifikator
            };

            if (dtoPerson.reservasjonSpecified)
                person.Reservasjon = TilDomeneObjekt(dtoPerson.reservasjon).Value;
            person.SikkerDigitalPostAdresse = TilDomeneObjekt(dtoPerson.SikkerDigitalPostAdresse);
            if (dtoPerson.statusSpecified)
                person.Status = TilDomeneObjekt(dtoPerson.status);
            person.X509Sertifikat = TilDomeneObjekt(dtoPerson.X509Sertifikat);
            if (dtoPerson.varslingsstatusSpecified)
                person.Varslingsstatus = (Varslingsstatus) Enum.Parse(typeof (Varslingsstatus), dtoPerson.varslingsstatus.ToString());

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

        private static SikkerDigitalPostAdresse TilDomeneObjekt(DTO.SikkerDigitalPostAdresse dtoSikkerDigitalPostAdresse)
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

        private static bool? TilDomeneObjekt(reservasjon reservasjon)
        {
            return reservasjon == reservasjon.JA;
        }

        private static Kontaktinformasjon TilDomeneObjekt(DTO.Kontaktinformasjon dtoKontaktinformasjon)
        {
            if (dtoKontaktinformasjon == null)
                return null;

            var kontaktinformasjon = new Kontaktinformasjon
            {
                Epostadresse = TilDomeneObjekt(dtoKontaktinformasjon.Epostadresse),
                Mobiltelefonnummer = TilDomeneObjekt(dtoKontaktinformasjon.Mobiltelefonnummer)
            };

            return kontaktinformasjon;
        }

        private static Mobiltelefonnummer TilDomeneObjekt(DTO.Mobiltelefonnummer dtoMobiltelefonnummer)
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

        private static Epostadresse TilDomeneObjekt(DTO.Epostadresse dtoEpostadresse)
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

        internal static PersonerSvar TilDomeneObjekt(HentPersonerRespons dtoHentPersonerRespons)
        {
            var personerSvar = new PersonerSvar {Personer = TilDomeneObjekt(dtoHentPersonerRespons.Person)};

            return personerSvar;
        }

        internal static PrintSertifikatSvar TilDomeneObjekt(HentPrintSertifikatRespons result)
        {
            var printSertifikatSvar = new PrintSertifikatSvar
            {
                Printsertifikat = TilDomeneObjekt(result.X509Sertifikat),
                PostkasseleverandørAdresse = result.postkasseleverandoerAdresse
            };

            return printSertifikatSvar;
        }
    }
}