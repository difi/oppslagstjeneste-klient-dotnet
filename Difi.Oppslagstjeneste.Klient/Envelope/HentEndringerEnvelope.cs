using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class HentEndringerEnvelope : OppslagstjenesteEnvelope
    {
        private long _fraEndringsNummer;
        private Informasjonsbehov _informasjonsbehov;

        public HentEndringerEnvelope(OppslagstjenesteInstillinger instillinger, long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
            : base(instillinger)
        {
            _fraEndringsNummer = fraEndringsNummer;
            _informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            // HentEndringerForespoersel
            var hentEndringer = Document.CreateElement("ns", "HentEndringerForespoersel", Navnerom.krr);
            hentEndringer.SetAttribute("fraEndringsNummer", _fraEndringsNummer.ToString());
            body.AppendChild(hentEndringer);

            foreach (Informasjonsbehov info in Enum.GetValues(typeof(Informasjonsbehov)))
            {
                if (_informasjonsbehov.HasFlag(info))
                {
                    var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.krr);
                    node.InnerText = info.ToString();
                    hentEndringer.AppendChild(node);
                }
            }

            return body;
        }

    }
}
