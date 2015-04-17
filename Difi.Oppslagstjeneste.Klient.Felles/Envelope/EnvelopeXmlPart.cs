using System.Xml;

namespace Difi.Felles.Envelope
{
    public abstract class EnvelopeXmlPart
    {
        protected XmlDocument Context { get; private set; }
        protected EnvelopeSettings Settings { get; private set; }

        protected EnvelopeXmlPart(EnvelopeSettings settings, XmlDocument context)
        {
            Settings = settings;
            Context = context;
        }

        public abstract XmlNode Xml();
    }
}
