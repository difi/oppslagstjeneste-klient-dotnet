using System;
using System.Collections.Generic;
using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Utilities.CompareObjects;
using Digipost.Signature.Api.Client.Core.Tests.Utilities.CompareObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
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

            private EndringerSvar DomeneEndringerSvar(HentEndringerRespons hentEndringerRespons)
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
                var kilde = new HentPersonerRespons {Person = new Person[1]};
                kilde.Person[0] = Person(DateTime.Now, DateTime.Now);
                return kilde;
            }

            private static HentEndringerRespons HentEndringerResponsTestdata(int antallPersoner)
            {
                var kilde = new HentEndringerRespons();
                var personer = new List<Person>();
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

            private static Domene.Entiteter.Person DomenePerson(Person kilde)
            {
                var forventet = new Domene.Entiteter.Person
                {
                    Kontaktinformasjon = new Domene.Entiteter.Kontaktinformasjon
                    {
                        Epostadresse =
                            new Domene.Entiteter.Epostadresse
                            {
                                Epost = kilde.Kontaktinformasjon.Epostadresse.Value,
                                SistOppdatert = kilde.Kontaktinformasjon.Epostadresse.sistOppdatert,
                                SistVerifisert = kilde.Kontaktinformasjon.Epostadresse.sistVerifisert
                            },
                        Mobiltelefonnummer = new Domene.Entiteter.Mobiltelefonnummer
                        {
                            SistVerifisert = kilde.Kontaktinformasjon.Mobiltelefonnummer.sistVerifisert,
                            SistOppdatert = kilde.Kontaktinformasjon.Mobiltelefonnummer.sistOppdatert,
                            Nummer = kilde.Kontaktinformasjon.Mobiltelefonnummer.Value
                        }
                    },
                    Personidentifikator = kilde.personidentifikator,
                    Reservasjon = false,
                    SikkerDigitalPostAdresse = new Domene.Entiteter.SikkerDigitalPostAdresse
                    {
                        PostkasseleverandørAdresse = kilde.SikkerDigitalPostAdresse.postkasseleverandoerAdresse,
                        Postkasseadresse = kilde.SikkerDigitalPostAdresse.postkasseadresse
                    },
                    Status = (Status) Enum.Parse(typeof (Status), kilde.status.ToString()),
                    X509Sertifikat = null
                };
                return forventet;
            }

            private static IEnumerable<Person> Personer(int antall, DateTime sistOppdatert, DateTime sistVerifisert)
            {
                var personer = new List<Person>();
                for (var i = 0; i < antall; i++)
                {
                    personer.Add(Person(sistOppdatert,sistVerifisert));
                }
                return personer;
            }

            private static Person Person(DateTime sistOppdatert, DateTime sistVerifisert)
            {
                var kilde = new Person
                {
                    status = status.AKTIV,
                    Kontaktinformasjon = new Kontaktinformasjon
                    {
                        Epostadresse = new Epostadresse
                        {
                            sistOppdatert = sistOppdatert,
                            Value = "epost",
                            sistVerifisert = sistVerifisert
                        },
                        Mobiltelefonnummer = new Mobiltelefonnummer
                        {
                            sistOppdatert = sistOppdatert,
                            Value = "mobil",
                            sistVerifisert = sistVerifisert
                        }
                    },
                    personidentifikator = "personIdentifikator",
                    reservasjon = reservasjon.NEI,
                    SikkerDigitalPostAdresse = new SikkerDigitalPostAdresse
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