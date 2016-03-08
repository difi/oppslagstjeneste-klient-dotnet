using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.Tester.Fakes
{

    public class FakeHttpClientReturningServerErrorResponse : FakeHttpClientHandler
    {
        public FakeHttpClientReturningServerErrorResponse(string response, HttpStatusCode httpStatusCode)
        {
            ResultCode = httpStatusCode;
            FakeResponse = response;
        }

        private string FakeResponse { get;}

        public override HttpContent GetContent()
        {
            
            return new StringContent(FakeResponse);
        }
    }

}
