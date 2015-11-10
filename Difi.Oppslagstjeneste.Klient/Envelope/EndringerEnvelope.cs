using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class EndringerEnvelope : OppslagstjenesteEnvelope
    {
        private readonly long _fraEndringsNummer;
        private readonly Informasjonsbehov _informasjonsbehov;

        public EndringerEnvelope(OppslagstjenesteInstillinger instillinger, long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
            : base(instillinger)
        {   
            _fraEndringsNummer = fraEndringsNummer;
            _informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            // HentEndringerForespoersel
            var hentEndringer = Document.CreateElement("ns", "HentEndringerForespoersel", Navnerom.OppslagstjenesteDefinisjon);
            hentEndringer.SetAttribute("fraEndringsNummer", _fraEndringsNummer.ToString());
            body.AppendChild(hentEndringer);

            foreach (Informasjonsbehov info in Enum.GetValues(typeof(Informasjonsbehov)))
            {
                if (_informasjonsbehov.HasFlag(info))
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
