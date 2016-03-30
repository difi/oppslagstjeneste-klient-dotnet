using System;
using Difi.Felles.Utility.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class ValidationException : DifiException
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}