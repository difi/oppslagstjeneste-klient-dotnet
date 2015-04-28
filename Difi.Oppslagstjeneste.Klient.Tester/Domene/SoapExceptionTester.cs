using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tester.Domene
{
    [TestClass]
    public class SoapExceptionTester
    {
        [TestMethod]
        public void ParseSoapExceptionSuksess()
        {
            try
            {
                var exception = new SoapException(Feilmelding());
            }
            catch
            {
                Assert.Fail();
            }
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
