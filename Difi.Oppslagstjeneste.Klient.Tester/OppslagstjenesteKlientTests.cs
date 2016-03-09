using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ApiClientShared;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tester.Fakes;
using Difi.Oppslagstjeneste.Klient.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Difi.Oppslagstjeneste.Klient.Tester
{
    [TestClass]
    public class OppslagstjenesteKlientTests
    {
        [TestClass]
        public class KonstruktørMethod : OppslagstjenesteKlientTests
        {
            [TestMethod]
            public void OppslagstjenesteKlientHandlesGenericSoapFaultCorrectly()
            {
                //Arrange
                var avsenderEnhetstesterSertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var resourceUtility = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Tester.Ressurser.Eksempler.Respons");
                var respons = Encoding.UTF8.GetString(resourceUtility.ReadAllBytes(true, "Feilmelding.xml"));
                //Act
                try
                {
                    var oppslagstjenesteKlientMock = OppslagstjenesteKlientMock(respons, HttpStatusCode.Forbidden, avsenderEnhetstesterSertifikat);

                    var client = oppslagstjenesteKlientMock.Object;
                    client.HentPersoner(new[] {"31108412345"}, Informasjonsbehov.Person);
                }
                catch (AggregateException e)
                {
                    var ex = e.InnerExceptions.ElementAt(0);
                    //Assert
                    Assert.IsTrue(ex.GetType() == typeof (SoapException));
                    var soapException = ex as SoapException;
                    Assert.AreEqual("env:Sender", soapException.Skyldig);
                    Assert.AreEqual("Invalid service usage: Service owner 988015814 does not have access to ENDRINGSTJENESTEN", soapException.Beskrivelse);
                }
            }
            
            private static Mock<OppslagstjenesteKlient> OppslagstjenesteKlientMock(string respons, HttpStatusCode httpStatusCode, X509Certificate2 avsenderEnhetstesterSertifikat)
            {
                var mockProxy = CreateOppslagstjenesteProxyMock(respons, httpStatusCode);
                var oppslagstjenesteKlientMock = new Mock<OppslagstjenesteKlient>(avsenderEnhetstesterSertifikat, new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø));
                oppslagstjenesteKlientMock.Setup(f => f.GetClient()).Returns(mockProxy.Object);
                return oppslagstjenesteKlientMock;
            }

            private static Mock<OppslagstjenesteHelper> CreateOppslagstjenesteProxyMock(string response, HttpStatusCode httpStatusCode)
            {
                var oppslagstjenesteMock = new Mock<OppslagstjenesteHelper>(new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø), new OppslagstjenesteInstillinger());
                var fakeHttpClientResponse = new FakeHttpClientReturningServerErrorResponse(response, httpStatusCode);
                oppslagstjenesteMock.Setup(
                    f =>
                        f.Client())
                    .Returns(new HttpClient(fakeHttpClientResponse));

                return oppslagstjenesteMock;
            }

            [TestMethod]
            public void KonstruktørMedSertifikater()
            {
                //Arrange
                var avsenderEnhetstesterSertifikat = DomeneUtility.GetAvsenderEnhetstesterSertifikat();
                var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø);

                //Act
                var oppslagstjenesteKlient = new OppslagstjenesteKlient(
                    avsenderEnhetstesterSertifikat,
                    oppslagstjenesteKonfigurasjon
                    );

                //Assert
                Assert.AreEqual(avsenderEnhetstesterSertifikat,
                    oppslagstjenesteKlient.OppslagstjenesteInstillinger.Avsendersertifikat);
                Assert.AreEqual(oppslagstjenesteKonfigurasjon, oppslagstjenesteKlient.OppslagstjenesteKonfigurasjon);
            }
        }
    }
}