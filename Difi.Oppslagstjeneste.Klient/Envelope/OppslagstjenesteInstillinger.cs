using System.Security.Cryptography.X509Certificates;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class OppslagstjenesteInstillinger : EnvelopeSettings
    {
        /// <summary>
        ///     Sertifikat som bl.a. benyttes for å signere utgående meldinger. Må inneholde en privatnøkkel.
        /// </summary>
        public X509Certificate2 Avsendersertifikat { get; set; }
    }
}