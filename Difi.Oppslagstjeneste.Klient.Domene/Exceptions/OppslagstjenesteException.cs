using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class OppslagstjenesteException : Exception
    {
        public OppslagstjenesteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}