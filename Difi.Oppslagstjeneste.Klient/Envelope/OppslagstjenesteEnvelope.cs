using System;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;
using Difi.Oppslagstjeneste.Klient.Felles.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class OppslagstjenesteEnvelope : AbstractEnvelope
    {
        protected XmlElement Security { get; private set; }

        protected OppslagstjenesteInstillinger Instillinger
        {
            get
            {
                return base.Settings as OppslagstjenesteInstillinger;
            }
        }

        protected OppslagstjenesteEnvelope(OppslagstjenesteInstillinger instillinger) : base(instillinger) { }

        protected override XmlElement CreateHeader()
        {
            var header = base.CreateHeader();
            Security = new Security(this.Settings, this.Document, TimeSpan.FromMinutes(30)).Xml() as XmlElement;
            header.AppendChild(Security);
            return header;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            // Legger til Id på body node for å kunne identifisere body element fra header-signaturen.
            var idAttribute = Document.CreateAttribute("wsu", "Id", Navnerom.wsu);
            idAttribute.Value = Settings.BodyId;
            body.SetAttributeNode(idAttribute);

            return body;
        }

        protected override void AddSignatureToHeader(XmlNode node)
        {
            SignedXml signed = new SignedXmlWithAgnosticId(Document, Instillinger.Sertifikat);

            signed.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";

            // Timestamp
            var tsReference = new Reference("#" + Settings.TimestampId);
            tsReference.AddTransform(new XmlDsigExcC14NTransform("wsse soapenv"));
            signed.AddReference(tsReference);

            // Body
            var bodyReference = new Reference("#" + Settings.BodyId);
            bodyReference.AddTransform(new XmlDsigExcC14NTransform(""));
            signed.AddReference(bodyReference);

            signed.KeyInfo.AddClause(new SecurityTokenReferenceClause(Instillinger.Sertifikat));
            signed.KeyInfo.Id = "KS-asdasdasdasd";

            signed.ComputeSignature();

            Security.AppendChild(Document.ImportNode(signed.GetXml(), true));
        }
    }
}
