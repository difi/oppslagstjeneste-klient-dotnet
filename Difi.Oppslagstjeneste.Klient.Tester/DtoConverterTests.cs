using System;
using System.Collections.Generic;
using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities.CompareObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epostadresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Epostadresse;
using Kontaktinformasjon = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Kontaktinformasjon;
using Mobiltelefonnummer = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Mobiltelefonnummer;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;
using SikkerDigitalPostAdresse = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.SikkerDigitalPostAdresse;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class DtoConverterTests
    {
        [TestClass]
        public class ToDomainObject : DtoConverterTests
        {
            [TestMethod]
            public void ConvertPerson()
            {
                //Arrange
                var source = new HentPersonerRespons { Person = new []
                {
                    GetDtoPerson(DateTime.Now, DateTime.Now)
                }};
        
                var expected = new PersonerSvar
                {
                    Personer = source.Person.Select(GetDomenePerson).ToList()
                };
               
                //Act
                var result = DtoConverter.ToDomainObject(source);

                //Assert
                var comparator = new Comparator();
                IEnumerable<IDifference> differences;
                comparator.AreEqual(expected, result, out differences);
                Assert.AreEqual(0, differences.Count());
            }

            [TestMethod]
            public void ConvertChanges()
            {
                //Arrange
                const int personsCount = 10;

                var source = new HentEndringerRespons()
                {
                    fraEndringsNummer = 0,
                    tilEndringsNummer = personsCount,
                    senesteEndringsNummer = personsCount
                };

                var persons = new List<DTO.Person>();
                for (var i = 0; i < personsCount; i++)
                {
                    persons.Add(GetDtoPerson(DateTime.Now, DateTime.Now));
                }
                source.Person = persons.ToArray();

                var expected = new EndringerSvar
                {
                    FraEndringsNummer = source.fraEndringsNummer,
                    SenesteEndringsNummer = source.senesteEndringsNummer,
                    TilEndringsNummer = source.tilEndringsNummer,
                    Personer = source.Person.Select(GetDomenePerson).ToList()
                };
                
                //Act
                var result = DtoConverter.ToDomainObject(source);

                //Assert
                var comparator = new Comparator();
                IEnumerable<IDifference> differences;
                comparator.AreEqual(expected, result, out differences);
                Assert.AreEqual(0, differences.Count());
            }
            
            private static Person GetDomenePerson(DTO.Person kilde)
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
            
            private static DTO.Person GetDtoPerson(DateTime sistOppdatert, DateTime sistVerifisert)
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