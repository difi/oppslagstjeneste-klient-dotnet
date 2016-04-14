using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
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
                var avsenderEnhetstesterSertifikat = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(miljø, avsenderEnhetstesterSertifikat);

                //Act

                //Assert
                Assert.AreEqual(miljø, oppslagstjenesteKonfigurasjon.Miljø);
                Assert.AreEqual(avsenderEnhetstesterSertifikat, oppslagstjenesteKonfigurasjon.Avsendersertifikat);
            }
        }
    }
}