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
        public class KonstruktørMethod : OppslagstjenesteValidatorTests
        {
            [TestMethod]
            public void EnkelKonstruktør()
            {
                //Arrange
                var requestXmlDocument = XmlResource.Request.GetPerson();
                var responseXmlDocument = XmlResource.Response.GetPerson();
                var avsendersertifikat = DomainUtility.GetSenderUnitTestCertificate();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsendersertifikat);

                var responseContainer = new ResponseContainer(responseXmlDocument);

                //Act
                var oppslagstjenestevalidator = new Oppslagstjenestevalidator(requestXmlDocument, responseContainer, oppslagstjenesteKonfigurasjon);

                //Assert
                Assert.AreEqual(requestXmlDocument, oppslagstjenestevalidator.SendtDokument);
                Assert.AreEqual(responseXmlDocument, responseContainer.Envelope);
                Assert.AreEqual(oppslagstjenesteKonfigurasjon.Miljø, oppslagstjenestevalidator.Miljø);
            }
        }
    }
}