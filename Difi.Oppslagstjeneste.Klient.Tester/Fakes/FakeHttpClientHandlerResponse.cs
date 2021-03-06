﻿using System.Net;
using System.Net.Http;

namespace Difi.Oppslagstjeneste.Klient.Tests.Fakes
{
    public class FakeHttpClientHandlerResponse : FakeHttpClientHandler
    {
        public FakeHttpClientHandlerResponse(string response, HttpStatusCode httpStatusCode)
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