using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using ApiClientShared.Enums;
using Difi.Oppslagstjeneste.Klient.Resources.Certificate;

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
            var bringThumbprint = "2d7f30dd05d3b7fc7ae5973a73f849083b2040ed";
            return CertificateUtility.SenderCertificate(bringThumbprint, Language.Norwegian);
        }

        internal static string GetOrganisasjonsnummerBring()
        {
            return "988015814";
        }
    }
}