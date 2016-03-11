using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class AbstractEnvelope
    {
        internal readonly EnvelopeSettings Settings;
        private bool _isCreated;

        protected AbstractEnvelope(EnvelopeSettings settings = null)
        {
            if (settings == null)
                settings = new EnvelopeSettings();
            Settings = settings;

            Document = new XmlDocument {PreserveWhitespace = true};

            var baseNode = Document.CreateElement("soap", "Envelope", Navnerom.SoapEnvelope12);
            Document.AppendChild(baseNode);

            var xmlDeclaration = Document.CreateXmlDeclaration("1.0", "UTF-8", null);
            Document.InsertBefore(xmlDeclaration, Document.DocumentElement);
        }

        public XmlDocument Document { get; }

        protected virtual XmlElement CreateHeader()
        {
            return Document.CreateElement("soap", "Header", Navnerom.SoapEnvelope12);
        }

        protected virtual XmlElement CreateBody()
        {
            return Document.CreateElement("soap", "Body", Navnerom.SoapEnvelope12);
        }

        /// <summary>
        ///     Metoden kalles etter at header og body er generert ferdig.
        /// </summary>
        /// <param name="node">Header elementet.</param>
        protected virtual void AddSignatureToHeader(XmlNode node)
        {
        }

        public XmlDocument ToXml()
        {
            if (!_isCreated)
            {
                var header = CreateHeader();
                Document.DocumentElement.AppendChild(header);
                Document.DocumentElement.AppendChild(CreateBody());
                AddSignatureToHeader(header);
                _isCreated = true;
            }
            return Document;
        }
    }
}