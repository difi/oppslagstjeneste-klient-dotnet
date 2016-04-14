using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Resources.Certificates;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities
{
    internal class DomainUtility
    {
        internal static readonly ResourceUtility ResourceUtility =
            new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tests.Testdata");

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