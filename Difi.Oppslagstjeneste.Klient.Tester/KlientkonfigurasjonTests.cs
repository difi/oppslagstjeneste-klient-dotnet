using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class KlientkonfigurasjonTests
    {
        [TestClass]
        public class KonstruktørMethod : KlientkonfigurasjonTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var miljø = Miljø.FunksjoneltTestmiljø;
                var proxyScheme = "https";
                var timeout = 30000;
                var avsendersertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var klientkonfigurasjon = new OppslagstjenesteKonfigurasjon(miljø, avsendersertifikat);
                

                //Act

                //Assert
                Assert.AreEqual(proxyScheme, klientkonfigurasjon.ProxyScheme);
                Assert.AreEqual(timeout, klientkonfigurasjon.TimeoutIMillisekunder);
                Assert.AreEqual(avsendersertifikat, klientkonfigurasjon.Avsendersertifikat);
            }
        }
    }
}