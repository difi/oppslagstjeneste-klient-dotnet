using System;
using Difi.Felles.Utility.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SendException : DifiException
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