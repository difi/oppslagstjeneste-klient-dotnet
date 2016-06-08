using Difi.Felles.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class MiljøTests
    {
        [TestMethod]
        public void ReturnsInitializedFunctionalTestEnvironmentVer1()
        {
            //Arrange
            var url = "https://kontaktinfo-ws-ver1.difi.no/kontaktinfo-external/ws-v5";
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon1;
            var certificates = CertificateChainUtility.FunksjoneltTestmiljøSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.AbsoluteUri);
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.SertifikatLager);
        }

        [TestMethod]
        public void ReturnsInitializedFunctionalTestEnvironmentVer2()
        {
            //Arrange
            var url = "https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v5";
            var environment = Miljø.FunksjoneltTestmiljøVerifikasjon2;
            var certificates = CertificateChainUtility.FunksjoneltTestmiljøSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.AbsoluteUri);
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.SertifikatLager);
        }

        [TestMethod]
        public void ReturnsInitializedProductionEnvironment()
        {
            //Arrange
            var url = "https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v5";
            var environment = Miljø.Produksjonsmiljø;
            var certificates = CertificateChainUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.CertificateChainValidator);
            Assert.AreEqual(url, environment.Url.ToString());
            CollectionAssert.AreEqual(certificates, environment.CertificateChainValidator.SertifikatLager);
        }
    }
}