using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;
using Difi.Oppslagstjeneste.Klient.Felles.Security;

namespace Difi.Oppslagstjenesten.Envelope
{
    public class OppslagstjenesteInstillinger : EnvelopeSettings
    {
        public OppslagstjenesteInstillinger() : base(SoapVersion.Soap11){ }

        /// <summary>
        /// Sertifikat som bl.a. benyttes for å signere utgående meldinger. Må inneholde en privatnøkkel.
        /// </summary>
        public X509Certificate2 Sertifikat { get; set; }

        /// <summary>
        /// Sertifikat som benyttes for å validere motatt svar. Oppslagstjenestens offentlige nøkkel.
        /// </summary>
        public X509Certificate2 Valideringssertifikat { get; set; }
    }
}
