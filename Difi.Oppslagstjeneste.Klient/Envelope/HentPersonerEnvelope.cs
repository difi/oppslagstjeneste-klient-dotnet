using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class HentPersonerEnvelope : OppslagstjenesteEnvelope
    {
        private string[] personidentifikator;
        private Informasjonsbehov _informasjonsbehov;
        
        public HentPersonerEnvelope(OppslagstjenesteInstillinger instillinger, string[] personidentifikator, Informasjonsbehov informasjonsbehov)
            : base(instillinger)
        {
            this.personidentifikator = personidentifikator;
            this._informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            var element = Document.CreateElement("ns", "HentPersonerForespoersel", Navnerom.OppslagstjenesteDefinisjon);

            foreach (Informasjonsbehov info in Enum.GetValues(typeof(Informasjonsbehov)))
            {
                if (_informasjonsbehov.HasFlag(info))
                {
                    var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                    node.InnerText = info.ToString();
                    element.AppendChild(node);
                }
            }

            foreach (var item in personidentifikator)
            {
                var node = Document.CreateElement("ns", "personidentifikator", Navnerom.OppslagstjenesteDefinisjon);
                node.InnerText = item;
                element.AppendChild(node);                
            }

            body.AppendChild(element);

            return body;
        }

    }
}
