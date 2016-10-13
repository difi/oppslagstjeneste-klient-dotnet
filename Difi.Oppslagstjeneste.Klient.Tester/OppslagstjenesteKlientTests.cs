using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    
    public class OppslagstjenesteKlientTests
    {
        
        public class ConstructorMethod : OppslagstjenesteKlientTests
        {
            [Fact]
            public void Initializes_with_certificates()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, senderUnitTestCertificate);

                //Act
                var oppslagstjenesteClient = new OppslagstjenesteKlient(oppslagstjenesteConfiguration);

                //Assert
                Assert.Equal(senderUnitTestCertificate, oppslagstjenesteClient.OppslagstjenesteKonfigurasjon.Avsendersertifikat);
                Assert.Equal(oppslagstjenesteConfiguration, oppslagstjenesteClient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}