using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class EndringerEnvelope : OppslagstjenesteEnvelope
    {
        internal long FraEndringsNummer { get; }

        internal Informasjonsbehov[] Informasjonsbehov { get; }

        public EndringerEnvelope(X509Certificate2 avsenderSertifikat, string sendPåVegneAv, long fraEndringsNummer,params Informasjonsbehov[] informasjonsbehov)
            : base(avsenderSertifikat,sendPåVegneAv)
        {
            FraEndringsNummer = fraEndringsNummer;
            Informasjonsbehov = informasjonsbehov;
            
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            // HentEndringerForespoersel
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