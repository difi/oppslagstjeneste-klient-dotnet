using System.Security.Cryptography.X509Certificates;
using Digipost.Api.Client.Shared.Resources.Resource;

namespace Difi.Oppslagstjeneste.Klient.Resources.Certificate
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

        internal static X509Certificate2 GetTestMottakerCertificateFromOppslagstjenesten()
        {
            var bytes = ResourceUtility.ReadAllBytes(true, "testmottakersertifikatFraOppslagstjenesten.pem");
            return new X509Certificate2(bytes);
        }

        internal static X509Certificate2 GetProductionMottakerCertificateFromOppslagstjenesten()
        {
            var bytes = ResourceUtility.ReadAllBytes(true, "produksjonsmottakersertifikatFraOppslagstjenesten.pem");
            return new X509Certificate2(bytes);
        }
    }
}