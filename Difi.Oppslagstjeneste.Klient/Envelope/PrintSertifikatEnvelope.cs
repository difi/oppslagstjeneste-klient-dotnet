
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class PrintSertifikatEnvelope : OppslagstjenesteEnvelope
    {
        public PrintSertifikatEnvelope(OppslagstjenesteInstillinger instillinger) : base(instillinger)
        {
        }

        protected override System.Xml.XmlElement CreateBody()
        {
            var body = base.CreateBody();
            var element = Document.CreateElement("ns", "HentPrintSertifikatForespoersel", Navnerom.OppslagstjenesteDefinisjon);
            body.AppendChild(element);
            return body;
        }
    }
}
