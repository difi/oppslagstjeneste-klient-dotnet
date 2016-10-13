using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Domene
{
    
    public class SoapExceptionTests
    {
        public static void ParseSoapExceptionSuksess()
        {
            //Arrange
            var expectedGuilty = "env:Sender";
            const string expectedDescription = "Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN";

            //Act
            var exception = new UventetFeilException(XmlResource.Response.GetSoapFault());

            //Assert
            Assert.Equal(expectedGuilty, exception.Skyldig.Trim());
            Assert.Equal(expectedDescription, exception.Beskrivelse.Trim());
        }
    }
}