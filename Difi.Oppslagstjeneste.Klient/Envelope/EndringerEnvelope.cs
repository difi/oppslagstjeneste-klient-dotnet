using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class EndringerEnvelope : OppslagstjenesteEnvelope
    {
        internal readonly long FraEndringsNummer;
        internal readonly Informasjonsbehov Informasjonsbehov;

        public EndringerEnvelope(OppslagstjenesteInstillinger instillinger, string sendPåVegneAv, long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
            : base(instillinger, sendPåVegneAv)
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

            foreach (Informasjonsbehov info in Enum.GetValues(typeof (Informasjonsbehov)))
            {
                if (Informasjonsbehov.HasFlag(info))
                {
                    var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                    node.InnerText = info.ToString();
                    hentEndringer.AppendChild(node);
                }
            }

            return body;
        }
    }
}