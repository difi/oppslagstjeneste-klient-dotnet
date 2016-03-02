using System.Security.Cryptography.X509Certificates;
using System.Xml;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.Tests
{
    internal class DomeneUtility
    {
        internal static readonly ResourceUtility ResourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.testdata");
        
        internal static X509Certificate2 GetAvsenderEnhetstesterSertifikat()
        {
            return EvigTestSertifikatMedPrivatnøkkel();
        }

        internal static X509Certificate2 GetMottakerEnhetstesterSertifikat()
        {
            return EvigTestSertifikatUtenPrivatnøkkel();
        }
        
        private static X509Certificate2 EvigTestSertifikatUtenPrivatnøkkel()
        {
            return new X509Certificate2(ResourceUtility.ReadAllBytes(true, "difi-enhetstester.cer"), "", X509KeyStorageFlags.Exportable);
        }

        private static X509Certificate2 EvigTestSertifikatMedPrivatnøkkel()
        {
            return new X509Certificate2(ResourceUtility.ReadAllBytes(true, "difi-enhetstester.p12"), "", X509KeyStorageFlags.Exportable);
        }
    }
}
