using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    [TestClass]
    public class OppslagstjenesteValidatorTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteValidatorTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler");
                var sendtTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Forespørsel", "HentPersonerForespørsel.xml"));
                var mottattTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Respons", "HentPersonerRespons.xml"));
                var sendtDokument = XmlUtility.TilXmlDokument(sendtTekst);
                var mottattDokument = XmlUtility.TilXmlDokument(mottattTekst);
                var oppslagstjenesteInstillinger = new OppslagstjenesteInstillinger();
                var miljø = Miljø.FunksjoneltTestmiljø;

                //Act
                var oppslagstjenestevalidator = new Oppslagstjenestevalidator(sendtDokument, mottattDokument, oppslagstjenesteInstillinger, miljø);

                //Assert
                Assert.AreEqual(sendtDokument, oppslagstjenestevalidator.SendtDokument);
                Assert.AreEqual(mottattDokument, oppslagstjenestevalidator.MottattDokument);
                Assert.AreEqual(oppslagstjenesteInstillinger, oppslagstjenestevalidator.OppslagstjenesteInstillinger);
                Assert.AreEqual(miljø, oppslagstjenestevalidator.Miljø);
            }
        }

        [TestClass]
        public class ValiderMethod : OppslagstjenesteValidatorTests
        {
            [Ignore]
            [TestMethod]
            public void ValidererGyldigXml()
            {
                //Arrange
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Testdata.Ressurser");
                var sendtTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonerRequest.xml"));
                var mottattTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "HentPersonerResponse.xml"));
                var sendtDokument = XmlUtility.TilXmlDokument(sendtTekst);
                var mottattDokument = XmlUtility.TilXmlDokument(mottattTekst);
                var oppslagstjenesteInstillinger = new OppslagstjenesteInstillinger();
                var miljø = Miljø.FunksjoneltTestmiljø;

                var oppslagstjenestevalidator = new OppslagstjenestevalidatorMedStubbetSjekkTimestamp(sendtDokument,
                    mottattDokument, oppslagstjenesteInstillinger, miljø);

                //Act
                oppslagstjenestevalidator.Valider();

                //Assert
                //Feiler om validering feiler.
            }
        }
    }
}