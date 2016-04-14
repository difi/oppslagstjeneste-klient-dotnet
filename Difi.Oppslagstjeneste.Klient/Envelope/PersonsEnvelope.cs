using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class PersonsEnvelope : OppslagstjenesteEnvelope
    {
        public PersonsEnvelope(X509Certificate2 senderCertificate, string sendOnBehalfOf, string[] personId, params Informasjonsbehov[] informationNeeds)
            : base(senderCertificate, sendOnBehalfOf)
        {
            PersonId = personId;
            InformationNeeds = informationNeeds;
        }

        internal Informasjonsbehov[] InformationNeeds { get; }

        internal string[] PersonId { get; }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var element = Document.CreateElement("ns", "HentPersonerForespoersel", Navnerom.OppslagstjenesteDefinisjon);

            foreach (var informasjonsbehov in InformationNeeds)
            {
                var node = Document.CreateElement("ns", "informasjonsbehov", Navnerom.OppslagstjenesteDefinisjon);
                node.InnerText = informasjonsbehov.ToString();
                element.AppendChild(node);
            }

            foreach (var item in PersonId)
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