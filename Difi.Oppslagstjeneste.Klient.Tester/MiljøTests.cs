using Difi.Felles.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class MiljøTests
    {
        [TestMethod]
        public void ReturnsInitializedFunctionalTestEnvironment()
        {
            //Arrange
            var url = "https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v5";
            var environment = Miljø.FunksjoneltTestmiljø;
            var certificates = SertifikatkjedeUtility.FunksjoneltTestmiljøSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.Sertifikatkjedevalidator);
            Assert.AreEqual(url, environment.Url.AbsoluteUri);
            CollectionAssert.AreEqual(certificates, environment.Sertifikatkjedevalidator.SertifikatLager);
        }

        [TestMethod]
        public void ReturnsInitializedProductionEnvironment()
        {
            //Arrange
            var url = "https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v5";
            var environment = Miljø.Produksjonsmiljø;
            var certificates = SertifikatkjedeUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(environment.Sertifikatkjedevalidator);
            Assert.AreEqual(url, environment.Url.ToString());
            CollectionAssert.AreEqual(certificates, environment.Sertifikatkjedevalidator.SertifikatLager);
        }
    }
}