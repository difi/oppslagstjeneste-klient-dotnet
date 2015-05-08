using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SendException : OppslagstjenesteException
    {
        public SendException()
        {
        }

        public SendException(string message)
            : base(message)
        {
        }

        public SendException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
