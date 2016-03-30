using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Integrasjon
{
    [TestClass]
    public class Integrasjonstester
    {
        private static OppslagstjenesteKlient _oppslagstjenesteKlient;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Sertifikater");
            var avsendersertifikat = new X509Certificate2(resourceUtility.ReadAllBytes(true, "DifiTestsertifikatKlient.p12"), "changeit", X509KeyStorageFlags.Exportable);
            var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø,avsendersertifikat);
            
            _oppslagstjenesteKlient = new OppslagstjenesteKlient( oppslagstjenesteKonfigurasjon);
        }

        [TestMethod]
        public void HentPersonerSuksess()
        {
            //Arrange

            //Act
            var personer = _oppslagstjenesteKlient.HentPersoner(new[] {"08077000292"},
                Informasjonsbehov.Sertifikat ,
                Informasjonsbehov.Kontaktinfo ,
                Informasjonsbehov.SikkerDigitalPost);

            //Assert
            Assert.IsTrue(personer.Any());
        }

        [TestMethod]
        public void HentEndringerSuksess()
        {
            //Arrange

            //Act
            var endringer = _oppslagstjenesteKlient.HentEndringer(886730,
                Informasjonsbehov.Kontaktinfo ,
                Informasjonsbehov.Sertifikat ,
                Informasjonsbehov.SikkerDigitalPost ,
                Informasjonsbehov.Person);

            //Assert
            Assert.IsTrue(endringer.Personer.Any());
        }

        [TestMethod]
        public void HentPrintsertifikatSuksess()
        {
            var printSertifikat = _oppslagstjenesteKlient.HentPrintSertifikat();

            Assert.IsNotNull(printSertifikat);
        }
    }
}