using System;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Felles.Utility.Security;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public abstract class OppslagstjenesteEnvelope : AbstractEnvelope
    {
        internal readonly string SendPåVegneAv;

        protected OppslagstjenesteEnvelope(OppslagstjenesteInstillinger instillinger, string sendPåVegneAv)
            : base(instillinger)
        {
            SendPåVegneAv = sendPåVegneAv;
        }

        protected XmlElement Security { get; private set; }

        internal OppslagstjenesteInstillinger Instillinger => Settings as OppslagstjenesteInstillinger;

        protected override XmlElement CreateHeader()
        {
            var header = base.CreateHeader();
            Security = new Security(Settings, Document, TimeSpan.FromMinutes(30)).Xml() as XmlElement;

            Settings.BinarySecurityId = "X509-" + Guid.NewGuid();
            var securityToken = new SecurityTokenReferenceClause(Instillinger.Avsendersertifikat,
                Settings.BinarySecurityId);
            var binaryToken = securityToken.GetTokenXml();
            Security.AppendChild(Document.ImportNode(binaryToken, true));
            header.AppendChild(Security);
            if (!string.IsNullOrEmpty(SendPåVegneAv))
            {
                var oppslagstjenesten = SendPåVegneAvNode();
                header.AppendChild(oppslagstjenesten);
            }
            return header;
        }

        private XmlElement SendPåVegneAvNode()
        {
            var oppslagstjenesten = Document.CreateElement("Oppslagstjenesten", Navnerom.OppslagstjenesteDefinisjon);
            var paavegneav = Document.CreateElement("PaaVegneAv", Navnerom.OppslagstjenesteDefinisjon);
            paavegneav.InnerXml = SendPåVegneAv;
            oppslagstjenesten.AppendChild(paavegneav);

            return oppslagstjenesten;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            // Legger til Id på body node for å kunne identifisere body element fra header-signaturen.
            var idAttribute = Document.CreateAttribute("wsu", "Id", Navnerom.WssecurityUtility10);
            idAttribute.Value = Settings.BodyId;
            body.SetAttributeNode(idAttribute);

            return body;
        }

        protected override void AddSignatureToHeader(XmlNode node)
        {
            SignedXml signed = new SignedXmlWithAgnosticId(Document, Instillinger.Avsendersertifikat);

            signed.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";

            // Timestamp
            var tsReference = new Reference("#" + Settings.TimestampId);
            signed.AddReference(tsReference);

            // Body
            var bodyReference = new Reference("#" + Settings.BodyId);
            bodyReference.AddTransform(new XmlDsigExcC14NTransform(""));
            signed.AddReference(bodyReference);

            var securityToken = new SecurityTokenReferenceClause(Settings.BinarySecurityId);

            signed.KeyInfo.AddClause(securityToken);
            signed.KeyInfo.Id = string.Format("KS-{0}", Guid.NewGuid());

            signed.ComputeSignature();

            Security.AppendChild(Document.ImportNode(signed.GetXml(), true));
        }
    }
}