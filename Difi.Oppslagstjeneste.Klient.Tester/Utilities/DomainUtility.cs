using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Resources.Certificate;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities
{
    internal class DomainUtility
    {
        internal static X509Certificate2 GetSenderUnitTestCertificate()
        {
            return CertificateResource.GetEternalTestCertificateWithPrivateKey();
        }

        internal static X509Certificate2 GetReceiverUnitTestCertificate()
        {
            return CertificateResource.GetEternalTestCertificateWithoutPrivateKey();
        }
    }
}