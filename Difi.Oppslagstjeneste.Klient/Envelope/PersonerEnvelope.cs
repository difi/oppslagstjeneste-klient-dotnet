using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public sealed class PersonerEnvelope : OppslagstjenesteEnvelope
    {
        internal Informasjonsbehov Informasjonsbehov { get; }

        internal string[] Personidentifikator { get; }

        public PersonerEnvelope(OppslagstjenesteInstillinger instillinger, string sendPåVegneAv, string[] personidentifikator, Informasjonsbehov informasjonsbehov)
            : base(instillinger, sendPåVegneAv)
        {
            Personidentifikator = personidentifikator;
            Informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var element = Document.CreateElement("ns", "HentPersonerForespoersel", Navnerom.OppslagstjenesteDefinisjon);
            foreach (Informasjonsbehov info in Enum.GetValues(typeof (Informasjonsbehov)))
            {
                if (Informasjonsbehov.HasFlag(info))
                {
                    var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                    node.InnerText = info.ToString();
                    element.AppendChild(node);
                }
            }

            foreach (var item in Personidentifikator)
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