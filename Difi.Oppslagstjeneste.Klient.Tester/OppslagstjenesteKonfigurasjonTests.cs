using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class OppslagstjenesteKonfigurasjonTests
    {
        [TestClass]
        public class ConstructorMethod : OppslagstjenesteKonfigurasjonTests
        {
            [TestMethod]
            public void InitializesFields()
            {
                //Arrange
                var environment = Miljø.FunksjoneltTestmiljøVer1;
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(environment, senderUnitTestCertificate);

                //Act

                //Assert
                Assert.AreEqual(environment, oppslagstjenesteConfiguration.Miljø);
                Assert.AreEqual(senderUnitTestCertificate, oppslagstjenesteConfiguration.Avsendersertifikat);
            }
        }
    }
}