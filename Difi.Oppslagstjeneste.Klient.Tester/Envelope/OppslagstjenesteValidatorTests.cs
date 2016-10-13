using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Xunit;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    
    public class OppslagstjenesteValidatorTests
    {
        
        public class ConstructorMethod : OppslagstjenesteValidatorTests
        {
            [Fact]
            public void EnkelKonstruktør()
            {
                //Arrange
                var requestXmlDocument = XmlResource.Request.GetPerson();
                var responseXmlDocument = XmlResource.Response.GetPerson();
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, senderUnitTestCertificate);

                var responseContainer = new ResponseContainer(responseXmlDocument);

                //Act
                var oppslagstjenesteValidator = new OppslagstjenesteValidator(requestXmlDocument, responseContainer, oppslagstjenesteConfiguration);

                //Assert
                Assert.Equal(requestXmlDocument, oppslagstjenesteValidator.SentDocument);
                Assert.Equal(responseXmlDocument, responseContainer.Envelope);
                Assert.Equal(oppslagstjenesteConfiguration.Miljø, oppslagstjenesteValidator.Environment);
            }
        }
    }
}