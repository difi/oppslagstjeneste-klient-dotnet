using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Difi.Felles.Utility.Security;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal abstract class OppslagstjenesteEnvelope : AbstractEnvelope
    {
        internal string SendPåVegneAv { get; }

        protected OppslagstjenesteEnvelope(X509Certificate2 avsenderSertifikat,string sendPåVegneAv)

        {
            AvsenderSertifikat = avsenderSertifikat;
            SendPåVegneAv = sendPåVegneAv;
        }

        protected XmlElement Security { get; private set; }

        
        public X509Certificate2 AvsenderSertifikat { get; set; }
        protected override XmlElement CreateHeader()
        {
            var header = base.CreateHeader();

            Security = new Security(AvsenderSertifikat,Settings, Document, TimeSpan.FromMinutes(30)).Xml() as XmlElement;
            header.AppendChild(Security);

            if (!string.IsNullOrEmpty(SendPåVegneAv))
            {
                var sendPåVegneAvNode = SendPåVegneAvNode();
                header.AppendChild(sendPåVegneAvNode);
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
            SignedXml signed = new SignedXmlWithAgnosticId(Document, AvsenderSertifikat);

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