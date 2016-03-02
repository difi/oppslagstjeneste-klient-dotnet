using System.Linq;
using ApiClientShared;
using ApiClientShared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Integrasjon
{
    [TestClass]
    public class Integrasjonstester
    {
        private static OppslagstjenesteKlient _oppslagstjenesteKlient;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var klientinnstillinger = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø);

            var avsenderSertifikat = CertificateUtility.SenderCertificate("B0CB922214D11E8CE993838DB4C6D04C0C0970B8",
                Language.Norwegian);

            _oppslagstjenesteKlient = new OppslagstjenesteKlient(avsenderSertifikat, klientinnstillinger);
        }

        [TestMethod]
        public void HentPersonerSuksess()
        {
            //Arrange

            //Act
            var personer = _oppslagstjenesteKlient.HentPersoner(new[] {"08077000292"},
                Informasjonsbehov.Sertifikat |
                Informasjonsbehov.Kontaktinfo |
                Informasjonsbehov.SikkerDigitalPost);


            //Assert
            Assert.IsTrue(personer.Any());
        }

        [TestMethod]
        public void HentEndringerSuksess()
        {
            //Arrange

            //Act
            var endringer = _oppslagstjenesteKlient.HentEndringer(886730,
                Informasjonsbehov.Kontaktinfo |
                Informasjonsbehov.Sertifikat |
                Informasjonsbehov.SikkerDigitalPost |
                Informasjonsbehov.Person);

            //Assert
            Assert.IsTrue(endringer.Personer.Any());
        }

        [TestMethod]
        public void HentPrintsertifikatSuksess()
        {
            var printSertifikat = _oppslagstjenesteKlient.HentPrintSertifikat();

            Assert.IsNotNull(printSertifikat);
        }
    }
}