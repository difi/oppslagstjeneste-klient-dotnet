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

        internal static X509Certificate2 GetAvsenderTestCertificate()
        {
            return GetBringTestCertificate();
        }

        private static X509Certificate2 GetBringTestCertificate()
        {
            // Difi.Oppslagstjeneste.Klient.Resources/Certificate/Data/TestChain/bring.cer (CN=Digipost Testintegrasjon for Digital Post)
            // The root (Buypass Class 3 Test4 Root CA), intermediate (Buypass Class 3 Test4 CA 3) and this certificate (all located in same directory) need to be added to the Keychain Access (MacOS), Credential Manager (Windows 10) or equivalent and trusted for the test to run OK.
            var bringThumbprint = "4addc8e8dc962889cf52c145860a017844e6399e";
            return CertificateUtility.SenderCertificate(bringThumbprint);
        }

        internal static string GetOrganisasjonsnummerBring()
        {
            return "988015814";
        }
    }
}