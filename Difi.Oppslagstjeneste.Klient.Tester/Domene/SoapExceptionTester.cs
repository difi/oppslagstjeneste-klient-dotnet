using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
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
            var feilmelding = XmlResource.Response.GetSoapFault();
            _exception = new SoapException(feilmelding);
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