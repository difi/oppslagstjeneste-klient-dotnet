using System;

namespace Difi.Oppslagstjeneste.Klient.Domene.Exceptions
{
    public class XmlParseException : OppslagstjenesteException
    {
        private const string Ekstrainfo = " Sjekk inner exception for mer info.";

        public string Rådata { get; set; }

        public XmlParseException()
        {
        }

        public XmlParseException(string message )
            : base(message + Ekstrainfo)
        {
        }

        public XmlParseException(string message, Exception inner)
            : base(message + Ekstrainfo, inner)
        {
        }
    }
}
