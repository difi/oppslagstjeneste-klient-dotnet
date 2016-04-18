using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Resources.Xml;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    [TestClass]
    public class OppslagstjenesteValidatorTests
    {
        [TestClass]
        public class ConstructorMethod : OppslagstjenesteValidatorTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var requestXmlDocument = XmlResource.Request.GetPerson();
                var responseXmlDocument = XmlResource.Response.GetPerson();
                var senderUnitTestCertificate = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVer1, senderUnitTestCertificate);

                var responseContainer = new ResponseContainer(responseXmlDocument);

                //Act
                var oppslagstjenesteValidator = new OppslagstjenesteValidator(requestXmlDocument, responseContainer, oppslagstjenesteConfiguration);

                //Assert
                Assert.AreEqual(requestXmlDocument, oppslagstjenesteValidator.SentDocument);
                Assert.AreEqual(responseXmlDocument, responseContainer.Envelope);
                Assert.AreEqual(oppslagstjenesteConfiguration.Miljø, oppslagstjenesteValidator.Environment);
            }
        }
    }
}