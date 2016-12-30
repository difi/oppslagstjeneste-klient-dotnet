using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class ValidationException : OppslagstjenesteException
    {
        public ValidationException(string message)
            : base(message, null)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}