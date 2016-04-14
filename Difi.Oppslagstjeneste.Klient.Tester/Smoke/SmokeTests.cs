using System.Linq;
using System.Threading.Tasks;
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
        public async Task HentPersonerSuksess()
        {
            //Arrange

            //Act
            var personer = await _oppslagstjenesteKlient.HentPersonerAsynkront(new[] {"08077000292"},
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.SikkerDigitalPost);

            //Assert
            Assert.IsTrue(personer.Any());
        }

        [TestMethod]
        public async Task HentEndringerSuksess()
        {
            //Arrange

            //Act
            var endringer = await _oppslagstjenesteKlient.HentEndringerAsynkront(886730,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
                Informasjonsbehov.Person);

            //Assert
            Assert.IsTrue(endringer.Personer.Any());
        }

        [TestMethod]
        public async Task HentPrintsertifikatSuksess()
        {
            var printSertifikat = await _oppslagstjenesteKlient.HentPrintSertifikatAsynkront();

            Assert.IsNotNull(printSertifikat);
        }
    }
}