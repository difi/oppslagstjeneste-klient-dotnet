using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class KlientkonfigurasjonTests
    {
        [TestClass]
        public class KonstruktørMethod : KlientkonfigurasjonTests
        {
            [TestMethod] public void EnkelKonstruktør()
            {
                //Arrange
                var miljø = Miljø.FunksjoneltTestmiljø;
                string proxyScheme = "https";
                int timeout = 30000;
                OppslagstjenesteKonfigurasjon klientkonfigurasjon = new OppslagstjenesteKonfigurasjon(miljø);

                //Act

                //Assert
                Assert.AreEqual(proxyScheme,klientkonfigurasjon.ProxyScheme);
                Assert.AreEqual(timeout, klientkonfigurasjon.TimeoutIMillisekunder);
            }
        }
    }
}
