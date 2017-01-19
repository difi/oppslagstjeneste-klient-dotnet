using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    
    public class KlientkonfigurasjonTests
    {
        
        public class ConstructorMethod : KlientkonfigurasjonTests
        {
            [Fact]
            public void Initializes_fields()
            {
                //Arrange
                var environment = Miljø.FunksjoneltTestmiljøVerifikasjon1;
                var proxyScheme = "https";
                var timeout = 30000;
                var senderUnitTestCertificate = DomainUtility.GetSenderSelfSignedCertificate();
                var clientConfiguration = new OppslagstjenesteKonfigurasjon(environment, senderUnitTestCertificate);

                //Act

                //Assert
                Assert.Equal(proxyScheme, clientConfiguration.ProxyScheme);
                Assert.Equal(timeout, clientConfiguration.TimeoutIMillisekunder);
                Assert.Equal(senderUnitTestCertificate, clientConfiguration.Avsendersertifikat);
            }
        }
    }
}