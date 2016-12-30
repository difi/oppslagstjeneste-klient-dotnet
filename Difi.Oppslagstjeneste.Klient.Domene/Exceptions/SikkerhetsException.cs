using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SikkerhetsException : OppslagstjenesteException
    {
        public SikkerhetsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}