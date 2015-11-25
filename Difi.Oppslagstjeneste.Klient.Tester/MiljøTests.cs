using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester
{

    [TestClass]
    public class MiljøTests
    {
        [TestMethod]
        public void ReturnererInitialisertFunksjoneltTestmiljø()
        {
            //Arrange
            var url = "https://qaoffentlig.meldingsformidler.digipost.no/api/ebms";
            var miljø = Miljø.FunksjoneltTestmiljø;
            var sertifikater = SertifikatkjedeUtility.FunksjoneltTestmiljøSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(miljø.Sertifikatkjedevalidator);
            Assert.AreEqual(url, miljø.Url.AbsoluteUri);
            CollectionAssert.AreEqual(sertifikater, miljø.Sertifikatkjedevalidator.SertifikatLager);
        }

        [TestMethod]
        public void ReturnererInitialisertProduksjonsmiljø()
        {
            //Arrange
            var url = "https://meldingsformidler.digipost.no/api/ebms";
            var miljø = Miljø.Produksjonsmiljø;
            var sertifikater = SertifikatkjedeUtility.ProduksjonsSertifikater();

            //Act

            //Assert
            Assert.IsNotNull(miljø.Sertifikatkjedevalidator);
            Assert.AreEqual(url, miljø.Url.ToString());
            CollectionAssert.AreEqual(sertifikater, miljø.Sertifikatkjedevalidator.SertifikatLager);
        }


    }
}
