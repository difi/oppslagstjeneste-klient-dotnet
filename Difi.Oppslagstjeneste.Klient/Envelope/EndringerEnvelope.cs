using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class EndringerEnvelope : OppslagstjenesteEnvelope
    {
        public EndringerEnvelope(X509Certificate2 senderCertificate, string sendOnBehalfOf, long fraEndringsNummer, params Informasjonsbehov[] informasjonsbehov)
            : base(senderCertificate, sendOnBehalfOf)
        {
            FraEndringsNummer = fraEndringsNummer;
            Informasjonsbehov = informasjonsbehov;
        }

        internal long FraEndringsNummer { get; }

        internal Informasjonsbehov[] Informasjonsbehov { get; }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var hentEndringer = Document.CreateElement("ns", "HentEndringerForespoersel", Navnerom.OppslagstjenesteDefinisjon);
            hentEndringer.SetAttribute("fraEndringsNummer", FraEndringsNummer.ToString());
            body.AppendChild(hentEndringer);

            foreach (var informasjonsbehov in Informasjonsbehov)
            {
                var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                node.InnerText = informasjonsbehov.ToString();
                hentEndringer.AppendChild(node);
            }

            return body;
        }
    }
}