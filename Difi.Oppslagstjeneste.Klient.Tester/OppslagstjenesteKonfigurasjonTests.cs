using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    
    public class OppslagstjenesteKonfigurasjonTests
    {
        
        public class ConstructorMethod : OppslagstjenesteKonfigurasjonTests
        {
            [Fact]
            public void InitializesFields()
            {
                //Arrange
                const bool forventetLoggForespørselOgRespons = false;
                var environment = Miljø.FunksjoneltTestmiljøVerifikasjon1;
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(environment, senderUnitTestCertificate);

                //Act

                //Assert
                Assert.Equal(environment, oppslagstjenesteConfiguration.Miljø);
                Assert.Equal(senderUnitTestCertificate, oppslagstjenesteConfiguration.Avsendersertifikat);
                Assert.Equal(forventetLoggForespørselOgRespons, oppslagstjenesteConfiguration.LoggForespørselOgRespons);
            }
        }
    }
}