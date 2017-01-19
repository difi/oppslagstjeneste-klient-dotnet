using Difi.Felles.Utility.Utilities;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Security;
using Xunit;
using static Difi.Oppslagstjeneste.Klient.Tests.Utilities.DomainUtility;

namespace Difi.Oppslagstjeneste.Klient.Tests.Security
{
    public class SenderCertificateValidatorTests
    {
        public class ValidateAndThrowIfInvalidMethod
        {
            [Fact]
            public void No_exception_on_valid_certificate()
            {
                SenderCertificateValidator.ValidateAndThrowIfInvalid(
                    GetAvsenderTestCertificate(),
                    CertificateChainUtility.FunksjoneltTestmiljøSertifikater());
            }

            [Fact]
            public void Sertifikat_exception_on_invalid_certificate()
            {
                Assert.Throws<SertifikatException>(
                    () => SenderCertificateValidator.ValidateAndThrowIfInvalid(
                        GetAvsenderTestCertificate(),
                        CertificateChainUtility.ProduksjonsSertifikater())
                );
            }
        }
    }
}