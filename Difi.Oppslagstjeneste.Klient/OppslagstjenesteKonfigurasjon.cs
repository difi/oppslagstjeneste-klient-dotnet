using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using ApiClientShared.Enums;
using Difi.Felles.Utility;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon : GeneriskKlientkonfigurasjon
    {
        /// <param name="avsendersertifikat">
        ///     Brukes for å signere forespørselen mot Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        /// <param name="miljø">
        ///     Hvilket miljø av oppslagstjenesten klienten skal kjøre mot.
        /// </param>
        /// <param name="sendPåVegneAv">
        ///     Organisasjonsnummeret til bedriften man skal sende på vegne av.
        ///     For informasjon om dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        public OppslagstjenesteKonfigurasjon(Miljø miljø, X509Certificate2 avsendersertifikat, string sendPåVegneAv = null)
            : base(miljø)
        {
            Felles.Utility.Logger.TraceSource = new TraceSource("Difi.Oppslagstjeneste.Klient");
            Logger = Felles.Utility.Logger.TraceLogger();
            Avsendersertifikat = avsendersertifikat;
            SendPåVegneAv = sendPåVegneAv;
        }

        /// <param name="avsendersertifikatThumbprint">
        ///     Thumbprint til sertifikat Virksomhet bruker til å signere
        ///     forespørselen. For informasjon om hvordan du finner dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />.
        /// </param>
        /// <param name="miljø">
        ///     Hvilket miljø av oppslagstjenesten klienten skal kjøre mot.
        /// </param>
        /// <param name="sendPåVegneAv">
        ///     Organisasjonsnummeret til bedriften man skal sende på vegne av.
        ///     For informasjon om dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        public OppslagstjenesteKonfigurasjon(Miljø miljø, string avsendersertifikatThumbprint, string sendPåVegneAv = null)
            : this(miljø, CertificateUtility.SenderCertificate(avsendersertifikatThumbprint, Language.Norwegian), sendPåVegneAv)
        {
        }

        /// <summary>
        ///     Sertifikat som bl.a. benyttes for å signere utgående meldinger. Må inneholde en privatnøkkel.
        /// </summary>
        public X509Certificate2 Avsendersertifikat { get; set; }

        public string SendPåVegneAv { get; set; }
    }
}