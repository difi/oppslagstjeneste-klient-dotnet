using System;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Domene
{
    [TestClass]
    public class SoapExceptionTester
    {
        private static SoapException _exception;

        [ClassInitialize]
        public static void ParseSoapExceptionSuksess(TestContext context)
        {
            try
            {
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler");
                var feilmelding = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Respons", "Feilmelding.xml"));
                _exception = new SoapException(feilmelding);
            }
            catch(Exception e)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentSkyldigSuksess()
        {
            Assert.AreEqual("env:Sender", _exception.Skyldig.Trim());
        }

        [TestMethod]
        public void HentFeilmeldingSuksess()
        {
            const string expected = "Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN";

            Assert.AreEqual(expected, _exception.Beskrivelse.Trim());
        }
    }
}