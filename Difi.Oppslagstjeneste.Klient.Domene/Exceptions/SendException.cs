using System;
using Difi.Felles.Utility.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SendException : OppslagstjenesteException
    {
        public SendException(string message)
            : base(message, null)
        {
        }

        public SendException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}