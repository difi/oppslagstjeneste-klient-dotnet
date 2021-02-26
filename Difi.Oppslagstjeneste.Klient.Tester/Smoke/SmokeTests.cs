using System.Linq;
using System.Threading.Tasks;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Resources.Certificate;
using Xunit;
using Assert = Xunit.Assert;

namespace Difi.Oppslagstjeneste.Klient.Tests.Smoke
{
    
    public class SmokeTests
    {
        private static OppslagstjenesteKlient _oppslagstjenesteKlient;

        public SmokeTests()
        {
            var senderCertificate = CertificateResource.GetDifiTestCertificate();
            var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, senderCertificate);

            _oppslagstjenesteKlient = new OppslagstjenesteKlient(oppslagstjenesteConfiguration);
        }

        [Fact]
        [Trait("Category", "testWithCert")]
        public async Task Get_persons_success()
        {
            //Arrange
            var pid = "08077000292";

            //Act
            var personer = await _oppslagstjenesteKlient.HentPersonerAsynkront(new[] {pid},
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.SikkerDigitalPost);

            //Assert
            Assert.True(personer.Any());
        }

        [Fact]
        [Trait("Category", "testWithCert")]
        public async Task Get_changes_success()
        {
            //Arrange

            //Act
            var changes = await _oppslagstjenesteKlient.HentEndringerAsynkront(886730,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
                Informasjonsbehov.Person);

            //Assert
            Assert.True(changes.Personer.Any());
        }

        [Fact]
        [Trait("Category", "testWithCert")]
        public async Task Get_print_certificate_success()
        {
            var printCertificate = await _oppslagstjenesteKlient.HentPrintSertifikatAsynkront();

            Assert.NotNull(printCertificate);
        }
    }
}
