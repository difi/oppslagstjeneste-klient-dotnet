using System;
using System.Collections.Generic;
using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.DTO;
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
            public void TilDomeneObjekt()
            {
                var sistVerifisert = DateTime.Now;
                var sistOppdatert = DateTime.Now;
                //Arrange
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

                //Act
                var resultat = DtoKonverterer.TilDomeneObjekt(kilde);
                resultat.X509Sertifikat = null;//setter x509 sertifikatet til null slik at Kellerman comperator ikke går i stå
                
                //Assert
                var comperator = new Comparator();
                IEnumerable<IDifference> differences;
                var erLike = comperator.AreEqual(forventet, resultat, out differences);

                Assert.IsTrue(erLike, "Objektene er ikke like! antall forskjeller:" + differences.Count());
            }
        }
    }
}