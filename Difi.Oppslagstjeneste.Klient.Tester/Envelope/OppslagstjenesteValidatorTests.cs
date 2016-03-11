using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
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
                var responseDokument = new ResponseDokument(mottattDokument);

                //Act
                var oppslagstjenestevalidator = new Oppslagstjenestevalidator(sendtDokument, responseDokument, oppslagstjenesteInstillinger, miljø);

                //Assert
                Assert.AreEqual(sendtDokument, oppslagstjenestevalidator.SendtDokument);
                Assert.AreEqual(mottattDokument, responseDokument.Envelope);
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
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler");
                var sendtTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Forespørsel", "HentPersonerForespørsel_x1.xml"));
                var mottattTekst = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Respons", "HentPersonerRespons_x1.xml"));
                var sendtDokument = XmlUtility.TilXmlDokument(sendtTekst);
                var mottattDokument = XmlUtility.TilXmlDokument(mottattTekst);
                var oppslagstjenesteInstillinger = new OppslagstjenesteInstillinger();
                var miljø = Miljø.FunksjoneltTestmiljø;
                var responseDokument = new ResponseDokument(mottattDokument);

                var oppslagstjenesteValidator = new Oppslagstjenestevalidator(sendtDokument,
                    responseDokument, oppslagstjenesteInstillinger, miljø);

                //Act
                oppslagstjenesteValidator.Valider();

                //Assert
                //Feiler om validering feiler.
            }
        }
    }
}