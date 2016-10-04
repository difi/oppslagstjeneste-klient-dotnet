using Difi.Felles.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class MiljøTests
    {
        [TestMethod]
        public void Returns_initialized_functional_test_environment_ver1()
        {
            //Arrange
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon1;
            const string url = "https://kontaktinfo-ws-ver1.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.AbsoluteUri);
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.CertificateStore);
        }

        [TestMethod]
        public void Returns_initialized_functional_test_environment_ver2()
        {
            //Arrange
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon2;
            const string url = "https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.AbsoluteUri);
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.CertificateStore);
        }

        [TestMethod]
        public void Returns_initialized_production_environment()
        {
            //Arrange
            var environment = Miljø.Produksjonsmiljø;
            const string url = "https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v5";
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.ToString());
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.CertificateStore);
        }
    }
}