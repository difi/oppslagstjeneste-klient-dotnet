using System;
using System.Linq;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestClass]
        public class ToNorwegianStringMethod
        {
            [TestMethod]
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
