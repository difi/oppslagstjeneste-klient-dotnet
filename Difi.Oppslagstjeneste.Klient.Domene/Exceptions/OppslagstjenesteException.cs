using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public abstract class OppslagstjenesteException : Exception
    {
        protected OppslagstjenesteException()
        {
        }

        protected OppslagstjenesteException(string message)
            : base(message)
        {
        }

        protected OppslagstjenesteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
