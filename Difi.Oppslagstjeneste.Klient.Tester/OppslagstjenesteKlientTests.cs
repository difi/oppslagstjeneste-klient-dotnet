using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    [TestClass]
    public class OppslagstjenesteKlientTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteKlientTests
        {
            [TestMethod] public void KonstruktørMedSertifikater()
            {
                //Arrange
                var avsenderEnhetstesterSertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var mottakerEnhetstesterSertifikat = DomeneUtility.GetMottakerEnhetstesterSertifikat();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon();

                //Act
                var oppslagstjenesteKlient = new OppslagstjenesteKlient(
                    avsenderEnhetstesterSertifikat, 
                    mottakerEnhetstesterSertifikat,
                    oppslagstjenesteKonfigurasjon
                    );

                //Assert
                Assert.AreEqual(avsenderEnhetstesterSertifikat, oppslagstjenesteKlient.OppslagstjenesteInstillinger.Avsendersertifikat);
                Assert.AreEqual(mottakerEnhetstesterSertifikat, oppslagstjenesteKlient.OppslagstjenesteInstillinger.Valideringssertifikat);
                Assert.AreEqual(oppslagstjenesteKonfigurasjon, oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}