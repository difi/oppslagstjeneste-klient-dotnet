using System;
using System.Linq;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Extensions;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Extensions
{
    
    public class EnumExtensionsTests
    {
        
        public class ToNorwegianStringMethod
        {
            [Fact]
            public void Converts_all_enum_values()
            {
                var certificateValidationTypes = Enum.GetValues(typeof(CertificateValidationType)).Cast<CertificateValidationType>();

                foreach (var certificateValidationType in certificateValidationTypes)
                {
                    certificateValidationType.ToNorwegianString();
                }
            }
        }

    }
}
