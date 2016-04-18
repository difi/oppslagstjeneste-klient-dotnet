using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Tests.Fakes;
using Moq;

namespace Difi.Oppslagstjeneste.Klient.Tests.Utilities
{
    internal class MockUtility
    {
        public static Mock<OppslagstjenesteKlient> OppslagstjenesteKlientMock(string respons, HttpStatusCode httpStatusCode, X509Certificate2 avsenderEnhetstesterSertifikat)
        {
            var oppslagstjenesteConfiguration = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVer1, avsenderEnhetstesterSertifikat);
            var mockProxy = CreateOppslagstjenesteHelperMock(respons, httpStatusCode, oppslagstjenesteConfiguration);
            var oppslagstjenesteClientMock = new Mock<OppslagstjenesteKlient>(oppslagstjenesteConfiguration);
            oppslagstjenesteClientMock.Setup(f => f.GetClient()).Returns(mockProxy.Object);
            return oppslagstjenesteClientMock;
        }

        private static Mock<OppslagstjenesteHelper> CreateOppslagstjenesteHelperMock(string response, HttpStatusCode httpStatusCode, OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            var oppslagstjenesteMock = new Mock<OppslagstjenesteHelper>(oppslagstjenesteKonfigurasjon);
            var fakeHttpClientResponse = new FakeHttpClientHandlerResponse(response, httpStatusCode);
            oppslagstjenesteMock.Setup(f => f.Client()).Returns(new HttpClient(fakeHttpClientResponse));

            return oppslagstjenesteMock;
        }
    }
}