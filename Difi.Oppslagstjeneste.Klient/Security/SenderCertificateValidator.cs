using System.Security.Cryptography.X509Certificates;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Domene.Extensions;

namespace Difi.Oppslagstjeneste.Klient.Security
{
    public static class SenderCertificateValidator
    {
        public static void ValidateAndThrowIfInvalid(X509Certificate2 senderCertificate, X509Certificate2Collection allowedChainCertificates)
        {
            var valideringsResultat = CertificateValidator.ValidateCertificateAndChain(
                senderCertificate,
                allowedChainCertificates
            );

            if (valideringsResultat.Type != CertificateValidationType.Valid)
            {
                throw new SertifikatException($"Sertifikatet som brukes for avsender er ikke gyldig. Prøver du å sende med et testsertifikat i produksjonsmiljø eller omvendt? Grunnen er '{valideringsResultat.Type.ToNorwegianString()}', beskrivelse: '{valideringsResultat.Message}'");
            }
        }
    }
}