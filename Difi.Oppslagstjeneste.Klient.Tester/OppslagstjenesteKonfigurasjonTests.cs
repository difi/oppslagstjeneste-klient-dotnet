using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class OppslagstjenesteKonfigurasjonTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteKonfigurasjonTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                Miljø miljø = Miljø.FunksjoneltTestmiljø;
                Konfigurasjon konfigurasjon = new Konfigurasjon(miljø);

                //Act

                //Assert
                konfigurasjon.Miljø = miljø;
            }
        }
    }
}