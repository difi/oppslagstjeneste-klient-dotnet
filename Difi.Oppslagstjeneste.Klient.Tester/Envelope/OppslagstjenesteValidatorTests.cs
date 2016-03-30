using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Examples;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                var avsendersertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var oppslagstjenesteKonfigurasjon =  new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsendersertifikat);
                
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