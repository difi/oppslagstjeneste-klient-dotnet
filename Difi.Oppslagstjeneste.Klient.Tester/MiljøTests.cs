using Difi.Felles.Utility.Utilities;

using Xunit;
using Assert = Xunit.Assert;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    
    public class MiljøTests
    {
        [Fact]
        public void Returns_initialized_functional_test_environment_ver1()
        {
            //Arrange
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon1;
            const string url = "https://kontaktinfo-ws-ver1.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.Equal(url, environment.Url.AbsoluteUri);
            Assert.Equal(certificates, environment.GodkjenteKjedeSertifikater);
        }

        [Fact]
        public void Returns_initialized_functional_test_environment_ver2()
        {
            //Arrange
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon2;
            const string url = "https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.Equal(url, environment.Url.AbsoluteUri);
            Assert.Equal(certificates, environment.GodkjenteKjedeSertifikater);
        }

        [Fact]
        public void Returns_initialized_production_environment()
        {
            //Arrange
            var environment = Miljø.Produksjonsmiljø;
            const string url = "https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.Equal(url, environment.Url.ToString());
            Assert.Equal(certificates, environment.GodkjenteKjedeSertifikater);
        }
    }
}