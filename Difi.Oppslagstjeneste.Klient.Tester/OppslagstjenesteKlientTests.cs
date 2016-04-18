using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class OppslagstjenesteKlientTests
    {
        [TestClass]
        public class ConstructorMethod : OppslagstjenesteKlientTests
        {
            [TestMethod]
            public void InitializesWithCertificates()
            {
                //Arrange
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVer1, senderUnitTestCertificate);

                //Act
                var oppslagstjenesteClient = new OppslagstjenesteKlient(oppslagstjenesteConfiguration);

                //Assert
                Assert.AreEqual(senderUnitTestCertificate, oppslagstjenesteClient.OppslagstjenesteKonfigurasjon.Avsendersertifikat);
                Assert.AreEqual(oppslagstjenesteConfiguration, oppslagstjenesteClient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}