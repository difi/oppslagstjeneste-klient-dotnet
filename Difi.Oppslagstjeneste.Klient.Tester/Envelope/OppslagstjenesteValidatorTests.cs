using System.Text;
using ApiClientShared;
using Difi.Felles.Utility.Security;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    [TestClass]
    public class OppslagstjenesteValidatorTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteValidatorTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var requestXmlDocument = TestResourceUtility.Request.PersonRequest.AsXmlDocument();
                var responseXmlDocument = TestResourceUtility.Response.PersonResponse.AsXmlDocument();
                var oppslagstjenesteInstillinger = new OppslagstjenesteInstillinger();
                var miljø = Miljø.FunksjoneltTestmiljø;
                var responseContainer = new ResponseContainer(responseXmlDocument);

                //Act
                var oppslagstjenestevalidator = new Oppslagstjenestevalidator(requestXmlDocument, responseContainer, oppslagstjenesteInstillinger, miljø);

                //Assert
                Assert.AreEqual(requestXmlDocument, oppslagstjenestevalidator.SendtDokument);
                Assert.AreEqual(responseXmlDocument, responseContainer.Envelope);
                Assert.AreEqual(oppslagstjenesteInstillinger, oppslagstjenestevalidator.OppslagstjenesteInstillinger);
                Assert.AreEqual(miljø, oppslagstjenestevalidator.Miljø);
            }
        }
    }
}