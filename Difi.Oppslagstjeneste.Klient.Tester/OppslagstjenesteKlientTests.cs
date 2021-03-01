using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    public class OppslagstjenesteKlientTests
    {
        public class ConstructorMethod : OppslagstjenesteKlientTests
        {
            [Fact]
            [Trait("Category", "testWithCert")]
            public void Calls_sender_certificate_validation()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetAvsenderTestCertificate();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.Produksjonsmiljø, senderUnitTestCertificate);

                //Act
                Assert.Throws<SertifikatException>(() => new OppslagstjenesteKlient(oppslagstjenesteKonfigurasjon));
            }

            [Fact]
            [Trait("Category", "testWithCert")]
            public void Initializes_with_certificates()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetAvsenderTestCertificate();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, senderUnitTestCertificate);

                //Act
                var oppslagstjenesteKlient = new OppslagstjenesteKlient(oppslagstjenesteKonfigurasjon);

                //Assert
                Assert.Equal(senderUnitTestCertificate, oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon.Avsendersertifikat);
                Assert.Equal(oppslagstjenesteKonfigurasjon, oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}
