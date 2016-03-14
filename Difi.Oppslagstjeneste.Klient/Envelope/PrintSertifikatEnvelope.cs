using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public sealed class PrintSertifikatEnvelope : OppslagstjenesteEnvelope
    {
        public PrintSertifikatEnvelope(OppslagstjenesteInstillinger instillinger, string sendPåVegneAv)
            : base(instillinger, sendPåVegneAv)
        {
        }

        protected override XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var element = Document.CreateElement("ns", "HentPrintSertifikatForespoersel",
                Navnerom.OppslagstjenesteDefinisjon);
            body.AppendChild(element);
            return body;
        }
    }
}