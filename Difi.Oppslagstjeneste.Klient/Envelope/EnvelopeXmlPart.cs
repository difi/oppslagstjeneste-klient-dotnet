using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class EnvelopeXmlPart
    {
        protected EnvelopeXmlPart(EnvelopeSettings settings, XmlDocument context)
        {
            Settings = settings;
            Context = context;
        }

        protected XmlDocument Context { get; private set; }
        protected EnvelopeSettings Settings { get; private set; }

        public abstract XmlNode Xml();
    }
}