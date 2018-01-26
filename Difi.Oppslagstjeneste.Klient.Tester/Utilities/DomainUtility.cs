using System.Security.Cryptography.X509Certificates;

using Difi.Oppslagstjeneste.Klient.Resources.Certificate;
using Digipost.Api.Client.Shared.Certificate;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities
{
    internal class DomainUtility
    {
        internal static X509Certificate2 GetSenderSelfSignedCertificate()
        {
            return CertificateResource.GetEternalTestCertificateWithPrivateKey();
        }

        internal static X509Certificate2 GetReceiverSelfSignedCertificate()
        {
            return CertificateResource.GetEternalTestCertificateWithoutPrivateKey();
        }

        internal static X509Certificate2 GetAvsenderTestCertificate()
        {
            return GetBringTestCertificate();
        }

        private static X509Certificate2 GetBringTestCertificate()
        {
            var bringThumbprint = "88bdb74fadaed87f52d2f5c11aed607deb9700ba";
            return CertificateUtility.SenderCertificate(bringThumbprint);
        }

        internal static string GetOrganisasjonsnummerBring()
        {
            return "988015814";
        }
    }
}