using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class PersonerEnvelope : OppslagstjenesteEnvelope
    {
        internal Informasjonsbehov[] Informasjonsbehov { get; }

        internal string[] Personidentifikator { get; }

        public PersonerEnvelope(X509Certificate2 avsenderSertifikat, string sendPåVegneAv, string[] personidentifikator, params Informasjonsbehov[] informasjonsbehov)
            : base(avsenderSertifikat, sendPåVegneAv)
        {
            Personidentifikator = personidentifikator;
            Informasjonsbehov = informasjonsbehov;
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var element = Document.CreateElement("ns", "HentPersonerForespoersel", Navnerom.OppslagstjenesteDefinisjon);

            foreach (var informasjonsbehov in Informasjonsbehov)
            {
                var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                node.InnerText = informasjonsbehov.ToString();
                element.AppendChild(node);
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