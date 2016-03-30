using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Tester.Fakes;
using Moq;

namespace Difi.Oppslagstjeneste.Klient.Tester.Utilities
{
    internal class MockUtility
    {
        public static Mock<OppslagstjenesteKlient> OppslagstjenesteKlientMock(string respons, HttpStatusCode httpStatusCode, X509Certificate2 avsenderEnhetstesterSertifikat)
        {
            var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsenderEnhetstesterSertifikat);
            var mockProxy = CreateOppslagstjenesteHelperMock(respons, httpStatusCode, oppslagstjenesteKonfigurasjon);
            var oppslagstjenesteKlientMock = new Mock<OppslagstjenesteKlient>(oppslagstjenesteKonfigurasjon);
            oppslagstjenesteKlientMock.Setup(f => f.GetClient()).Returns(mockProxy.Object);
            return oppslagstjenesteKlientMock;
        }

        private static Mock<OppslagstjenesteHelper> CreateOppslagstjenesteHelperMock(string response, HttpStatusCode httpStatusCode,OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            var oppslagstjenesteMock = new Mock<OppslagstjenesteHelper>(oppslagstjenesteKonfigurasjon);
            var fakeHttpClientResponse = new FakeHttpClientHandlerResponse(response, httpStatusCode);
            oppslagstjenesteMock.Setup(f => f.Client())
                .Returns(new HttpClient(fakeHttpClientResponse));

            return oppslagstjenesteMock;
        }
    }
}