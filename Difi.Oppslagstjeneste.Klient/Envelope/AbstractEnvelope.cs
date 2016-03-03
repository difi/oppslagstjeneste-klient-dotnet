using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class AbstractEnvelope
    {
        private bool _isCreated;

        public AbstractEnvelope(EnvelopeSettings settings = null)
        {
            if (settings == null)
                settings = new EnvelopeSettings(SoapVersion.Soap12);
            Settings = settings;

            Document = new XmlDocument();
            Document.PreserveWhitespace = true;

            var baseNode = Document.CreateElement("soap", "Envelope", Navnerom.SoapEnvelope12);
            Document.AppendChild(baseNode);

            var xmlDeclaration = Document.CreateXmlDeclaration("1.0", "UTF-8", null);
            Document.InsertBefore(xmlDeclaration, Document.DocumentElement);
        }

        public XmlDocument Document { get; }

        public EnvelopeSettings Settings { get; set; }

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