using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Domene
{
    [TestClass]
    public class SoapExceptionTests
    {
        [ClassInitialize]
        public static void ParseSoapExceptionSuksess(TestContext context)
        {
            //Arrange
            var expectedGuilty = "env:Sender";
            const string expectedDescription = "Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN";

            //Act
            var exception = new SoapException(XmlResource.Response.GetSoapFault());

            //Assert
            Assert.AreEqual(expectedGuilty, exception.Skyldig.Trim());
            Assert.AreEqual(expectedDescription, exception.Beskrivelse.Trim());
        }
    }
}