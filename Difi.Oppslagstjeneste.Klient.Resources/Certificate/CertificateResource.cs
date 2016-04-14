using System.Security.Cryptography.X509Certificates;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.Resources.Certificates
{
    internal class CertificateResource
    {
        private static readonly ResourceUtility ResourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Resources.Certificate.Data");

        internal static X509Certificate2 GetDifiTestCertificate()
        {
            var bytes = ResourceUtility.ReadAllBytes(true, "DifiTestCertificateClient.p12");
            return new X509Certificate2(bytes, "changeit", X509KeyStorageFlags.Exportable);
        }

        internal static X509Certificate2 GetEternalTestCertificateWithoutPrivateKey()
        {
            var bytes = ResourceUtility.ReadAllBytes(true, "difi-enhetstester.cer");
            return new X509Certificate2(bytes, "", X509KeyStorageFlags.Exportable);
        }

        internal static X509Certificate2 GetEternalTestCertificateWithPrivateKey()
        {
            var bytes = ResourceUtility.ReadAllBytes(true, "difi-enhetstester.p12");
            return new X509Certificate2(bytes, "", X509KeyStorageFlags.Exportable);
        }
    }
}