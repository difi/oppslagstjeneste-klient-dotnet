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
                var miljø = Miljø.FunksjoneltTestmiljø;
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(miljø);

                //Act

                //Assert
                oppslagstjenesteKonfigurasjon.Miljø = miljø;
            }
        }
    }
}