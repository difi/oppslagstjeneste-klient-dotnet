using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class KlientkonfigurasjonTests
    {
        [TestClass]
        public class ConstructorMethod : KlientkonfigurasjonTests
        {
            [TestMethod]
            public void InitializesFields()
            {
                //Arrange
                var environment = Miljø.FunksjoneltTestmiljøVer1;
                var proxyScheme = "https";
                var timeout = 30000;
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var clientConfiguration = new OppslagstjenesteKonfigurasjon(environment, senderUnitTestCertificate);

                //Act

                //Assert
                Assert.AreEqual(proxyScheme, clientConfiguration.ProxyScheme);
                Assert.AreEqual(timeout, clientConfiguration.TimeoutIMillisekunder);
                Assert.AreEqual(senderUnitTestCertificate, clientConfiguration.Avsendersertifikat);
            }
        }
    }
}