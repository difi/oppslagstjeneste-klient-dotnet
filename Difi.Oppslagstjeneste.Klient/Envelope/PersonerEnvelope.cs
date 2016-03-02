using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class PersonerEnvelope : OppslagstjenesteEnvelope
    {
        private readonly Informasjonsbehov _informasjonsbehov;
        private readonly string[] personidentifikator;

        public PersonerEnvelope(OppslagstjenesteInstillinger instillinger, string[] personidentifikator,
            Informasjonsbehov informasjonsbehov)
            : base(instillinger)
        {
            this.personidentifikator = personidentifikator;
            _informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();

            var element = Document.CreateElement("ns", "HentPersonerForespoersel", Navnerom.OppslagstjenesteDefinisjon);

            foreach (Informasjonsbehov info in Enum.GetValues(typeof (Informasjonsbehov)))
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