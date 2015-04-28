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
                _exception = new SoapException(Feilmelding());
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HentSkyldigSuksess()
        {
            Assert.AreEqual("SOAP-ENV:Client", _exception.Skyldig.Trim());
        }

        [TestMethod]
        public void HentFeilmeldingSuksess()
        {
            var expected =
                "[4001] Input xml er ikke i henhold til xsd Feilinstanse:1e113062-dbef-499b-af58-7ce735d69882";

            Assert.AreEqual(expected,_exception.Beskrivelse.Trim());
        }

        private static string Feilmelding()
        {
            return "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
            "   <SOAP-ENV:Header/>" +
            "       <SOAP-ENV:Body>" +
            "           <SOAP-ENV:Fault>" +
            "               <faultcode>SOAP-ENV:Client</faultcode>" +
            "               <faultstring xml:lang=\"no\">" +
            "                   [4001] Input xml er ikke i henhold til xsd Feilinstanse:1e113062-dbef-499b-af58-7ce735d69882</faultstring>" +
            "           </SOAP-ENV:Fault>" +
            "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
        }
    }
}
