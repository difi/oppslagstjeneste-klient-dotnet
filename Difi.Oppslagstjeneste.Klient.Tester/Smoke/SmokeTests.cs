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
            var senderCertificate = CertificateResource.GetDifiTestCertificate();
            var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, senderCertificate);

            _oppslagstjenesteKlient = new OppslagstjenesteKlient(oppslagstjenesteConfiguration);
        }

        [TestMethod]
        public async Task GetPersonsSuccess()
        {
            //Arrange
            var pid = "08077000292";

            //Act
            var personer = await _oppslagstjenesteKlient.HentPersonerAsynkront(new[] {pid},
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.SikkerDigitalPost);

            //Assert
            Assert.IsTrue(personer.Any());
        }

        [TestMethod]
        public async Task GetChangesSuccess()
        {
            //Arrange

            //Act
            var changes = await _oppslagstjenesteKlient.HentEndringerAsynkront(886730,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
                Informasjonsbehov.Person);

            //Assert
            Assert.IsTrue(changes.Personer.Any());
        }

        [TestMethod]
        public async Task GetPrintCertificateSuccess()
        {
            var printCertificate = await _oppslagstjenesteKlient.HentPrintSertifikatAsynkront();

            Assert.IsNotNull(printCertificate);
        }
    }
}