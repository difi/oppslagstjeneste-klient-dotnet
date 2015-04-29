using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using ApiClientShared.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Integrasjon
{
    [TestClass]
    public class UnitTest1
    {
        private static OppslagstjenesteKlient _oppslagstjenesteKlient;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var konfig = new OppslagstjenesteKonfigurasjon
            {
                ServiceUri = new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4")
            };

            var avsenderSertifikat = CertificateUtility.SenderCertificate("B0CB922214D11E8CE993838DB4C6D04C0C0970B8", Language.Norwegian);
            var valideringssertifikat = CertificateUtility.ReceiverCertificate("a4 7d 57 ea f6 9b 62 77 10 fa 0d 06 ec 77 50 0b af 71 c4 32", Language.Norwegian);

            _oppslagstjenesteKlient = new OppslagstjenesteKlient(avsenderSertifikat, valideringssertifikat, konfig);
            
        }

        [TestMethod]
        public void HentPersonerSuksess()
        {
            try
            {
                var personer = _oppslagstjenesteKlient.HentPersoner(new string[] { "08077000292" },
                    Informasjonsbehov.Sertifikat |
                    Informasjonsbehov.Kontaktinfo |
                    Informasjonsbehov.SikkerDigitalPost);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentEndringerSuksess()
        {
            try
            {
                var endringer = _oppslagstjenesteKlient.HentEndringer(886730,
                    Informasjonsbehov.Kontaktinfo |
                    Informasjonsbehov.Sertifikat |
                    Informasjonsbehov.SikkerDigitalPost |
                    Informasjonsbehov.Person);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentPrintsertifikatSuksess()
        {
            try
            {
                var printSertifikat = _oppslagstjenesteKlient.HentPrintSertifikat();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
