using System.Net;
using System.Net.Http;

namespace Difi.Oppslagstjeneste.Klient.Tester.Fakes
{
    public class FakeHttpClientReturningServerErrorResponse : FakeHttpClientHandler
    {
        public FakeHttpClientReturningServerErrorResponse(string response, HttpStatusCode httpStatusCode)
        {
            ResultCode = httpStatusCode;
            FakeResponse = response;
        }

        private string FakeResponse { get; }

        public override HttpContent GetContent()
        {
            return new StringContent(FakeResponse);
        }
    }
}