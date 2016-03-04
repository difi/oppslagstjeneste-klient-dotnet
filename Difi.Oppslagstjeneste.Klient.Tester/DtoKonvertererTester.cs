using System;
using System.Collections.Generic;
using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities.CompareObjects;
using Digipost.Signature.Api.Client.Core.Tests.Utilities.CompareObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epostadresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Epostadresse;
using Kontaktinformasjon = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Kontaktinformasjon;
using Mobiltelefonnummer = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Mobiltelefonnummer;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;
using SikkerDigitalPostAdresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.SikkerDigitalPostAdresse;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class DtoKonvertererTester
    {
        [TestClass]
        public class TilDomeneObjektMethod : DtoKonvertererTester
        {
            [TestMethod]
            public void HentPersoner()
            {
                //Arrange
                var kilde = HentPersonerResponsTestdata();
                var forventet = DomenePersonSvar(kilde);

                //Act
                var resultat = DtoKonverterer.TilDomeneObjekt(kilde);

                //Assert
                var comperator = new Comparator();
                IEnumerable<IDifference> differences;
                var erLike = comperator.AreEqual(forventet, resultat, out differences);

                Assert.IsTrue(erLike, "Objektene er ikke like! antall forskjeller:" + differences.Count());
            }

            [TestMethod]
            public void HentEndringer()
            {
                //Arrange
                const int antallPersoner = 10;
                var hentEndringerRespons = HentEndringerResponsTestdata(antallPersoner);
                var forventet = DomeneEndringerSvar(hentEndringerRespons);

                //Act
                var resultat = DtoKonverterer.TilDomeneObjekt(hentEndringerRespons);

                //Assert
                var comperator = new Comparator();
                IEnumerable<IDifference> differences;
                var erLike = comperator.AreEqual(forventet, resultat, out differences);

                Assert.IsTrue(erLike, "Objektene er ikke like! antall forskjeller:" + differences.Count());
            }

            private static EndringerSvar DomeneEndringerSvar(HentEndringerRespons hentEndringerRespons)
            {
                var endringerSvar = new EndringerSvar
                {
                    FraEndringsNummer = hentEndringerRespons.fraEndringsNummer,
                    SenesteEndringsNummer = hentEndringerRespons.senesteEndringsNummer,
                    TilEndringsNummer = hentEndringerRespons.tilEndringsNummer,
                    Personer = hentEndringerRespons.Person.Select(DomenePerson).ToList()
                };
                return endringerSvar;
            }

            private static HentPersonerRespons HentPersonerResponsTestdata()
            {
                var kilde = new HentPersonerRespons {Person = new DTO.Person[1]};
                kilde.Person[0] = Person(DateTime.Now, DateTime.Now);
                return kilde;
            }

            private static HentEndringerRespons HentEndringerResponsTestdata(int antallPersoner)
            {
                var kilde = new HentEndringerRespons();
                var personer = new List<DTO.Person>();
                for (var i = 0; i < antallPersoner; i++)
                {
                    personer.Add(Person(DateTime.Now, DateTime.Now));
                }
                kilde.Person = personer.ToArray();

                kilde.fraEndringsNummer = 0;
                kilde.tilEndringsNummer = antallPersoner;
                kilde.senesteEndringsNummer = antallPersoner;

                return kilde;
            }

            private static PersonerSvar DomenePersonSvar(HentPersonerRespons hentPersonerRespons)
            {
                var personsvar = new PersonerSvar();
                var personer = hentPersonerRespons.Person.Select(DomenePerson).ToList();

                personsvar.Personer = personer;

                return personsvar;
            }

            private static Person DomenePerson(DTO.Person kilde)
            {
                var forventet = new Person
                {
                    Kontaktinformasjon = new Kontaktinformasjon
                    {
                        Epostadresse =
                            new Epostadresse
                            {
                                Epost = kilde.Kontaktinformasjon.Epostadresse.Value,
                                SistOppdatert = kilde.Kontaktinformasjon.Epostadresse.sistOppdatert,
                                SistVerifisert = kilde.Kontaktinformasjon.Epostadresse.sistVerifisert
                            },
                        Mobiltelefonnummer = new Mobiltelefonnummer
                        {
                            SistVerifisert = kilde.Kontaktinformasjon.Mobiltelefonnummer.sistVerifisert,
                            SistOppdatert = kilde.Kontaktinformasjon.Mobiltelefonnummer.sistOppdatert,
                            Nummer = kilde.Kontaktinformasjon.Mobiltelefonnummer.Value
                        }
                    },
                    Personidentifikator = kilde.personidentifikator,
                    Reservasjon = false,
                    SikkerDigitalPostAdresse = new SikkerDigitalPostAdresse
                    {
                        PostkasseleverandørAdresse = kilde.SikkerDigitalPostAdresse.postkasseleverandoerAdresse,
                        Postkasseadresse = kilde.SikkerDigitalPostAdresse.postkasseadresse
                    },
                    Status = (Status) Enum.Parse(typeof (Status), kilde.status.ToString()),
                    X509Sertifikat = null
                };
                return forventet;
            }

            private static IEnumerable<DTO.Person> Personer(int antall, DateTime sistOppdatert, DateTime sistVerifisert)
            {
                var personer = new List<DTO.Person>();
                for (var i = 0; i < antall; i++)
                {
                    personer.Add(Person(sistOppdatert, sistVerifisert));
                }
                return personer;
            }

            private static DTO.Person Person(DateTime sistOppdatert, DateTime sistVerifisert)
            {
                var kilde = new DTO.Person
                {
                    status = status.AKTIV,
                    Kontaktinformasjon = new DTO.Kontaktinformasjon
                    {
                        Epostadresse = new DTO.Epostadresse
                        {
                            sistOppdatert = sistOppdatert,
                            sistOppdatertSpecified = sistOppdatert != null,
                            Value = "epost",
                            sistVerifisert = sistVerifisert,
                            sistVerifisertSpecified = sistVerifisert != null
                        },
                        Mobiltelefonnummer = new DTO.Mobiltelefonnummer
                        {
                            sistOppdatert = sistOppdatert,
                            sistOppdatertSpecified = sistOppdatert != null,
                            Value = "mobil",
                            sistVerifisert = sistVerifisert,
                            sistVerifisertSpecified = sistVerifisert != null
                        }
                    },
                    personidentifikator = "personIdentifikator",
                    reservasjon = reservasjon.NEI,
                    SikkerDigitalPostAdresse = new DTO.SikkerDigitalPostAdresse
                    {
                        postkasseadresse = "postkasseadresse",
                        postkasseleverandoerAdresse = "postkasseleverandoerAdresse"
                    },
                    varslingsstatus = varslingsstatus.KAN_VARSLES,
                    X509Sertifikat = null
                };
                return kilde;
            }
        }
    }
}