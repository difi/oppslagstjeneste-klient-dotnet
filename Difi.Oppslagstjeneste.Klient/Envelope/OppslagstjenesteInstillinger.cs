using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class OppslagstjenesteInstillinger : EnvelopeSettings
    {
        public OppslagstjenesteInstillinger()
            : base(SoapVersion.Soap11)
        {
        }

        /// <summary>
        ///     Sertifikat som bl.a. benyttes for å signere utgående meldinger. Må inneholde en privatnøkkel.
        /// </summary>
        public X509Certificate2 Avsendersertifikat { get; set; }

        /// <summary>
        ///     Sertifikat som benyttes for å validere motatt svar. Oppslagstjenestens offentlige nøkkel.
        /// </summary>
        public X509Certificate2 Valideringssertifikat { get; set; }
    }
}