using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class SertifikatException : SikkerhetsException
    {
        public SertifikatException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public SertifikatException(string message)
            : base(message, null)
        {
        }
    }
}