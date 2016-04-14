using System.Linq;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Resources.Certificates;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Smoke
{
    [TestClass]
    public class SmokeTests
    {
        private static OppslagstjenesteKlient _oppslagstjenesteKlient;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            XmlConfigurator.Configure();
            var avsendersertifikat = CertificateResource.GetDifiTestCertificate();
            var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsendersertifikat);

            _oppslagstjenesteKlient = new OppslagstjenesteKlient(oppslagstjenesteKonfigurasjon);
        }

        [TestMethod]
        public void HentPersonerSuksess()
        {
            //Arrange

            //Act
            var personer = _oppslagstjenesteKlient.HentPersoner(new[] {"08077000292"},
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.Kontaktinfo,
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
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
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