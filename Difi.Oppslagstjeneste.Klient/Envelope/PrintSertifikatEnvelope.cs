using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class PrintSertifikatEnvelope : OppslagstjenesteEnvelope
    {
        public PrintSertifikatEnvelope(X509Certificate2 avsenderSertifikat, string sendPåVegneAv)
            : base(avsenderSertifikat, sendPåVegneAv)
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