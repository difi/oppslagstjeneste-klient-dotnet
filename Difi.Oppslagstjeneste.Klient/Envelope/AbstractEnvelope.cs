using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class AbstractEnvelope
    {
        public XmlDocument Document { get; private set; }
        public EnvelopeSettings Settings { get; set; }

        public AbstractEnvelope(EnvelopeSettings settings = null)
        {
            if (settings == null)
                settings = new EnvelopeSettings(SoapVersion.Soap12);
            this.Settings = settings;

            Document = new XmlDocument();
            Document.PreserveWhitespace = true;

            var baseNode = Document.CreateElement("soap", "Envelope", Settings.SoapNamespace);
            Document.AppendChild(baseNode);

            var xmlDeclaration = Document.CreateXmlDeclaration("1.0", "UTF-8", null);
            Document.InsertBefore(xmlDeclaration, Document.DocumentElement);
        }


        protected virtual XmlElement CreateHeader()
        {
            return Document.CreateElement("soap", "Header", Settings.SoapNamespace);
        }

        protected virtual XmlElement CreateBody()
        {
            return Document.CreateElement("soap", "Body", Settings.SoapNamespace);
        }

        /// <summary>
        /// Metoden kalles etter at header og body er generert ferdig.
        /// </summary>
        /// <param name="node">Header elementet.</param>
        protected virtual void AddSignatureToHeader(XmlNode node) { }

        private bool _isCreated = false;
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
