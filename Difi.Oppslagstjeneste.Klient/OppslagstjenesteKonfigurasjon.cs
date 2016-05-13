using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using ApiClientShared.Enums;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon
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
        {
            Avsendersertifikat = avsendersertifikat;
            SendPåVegneAv = sendPåVegneAv;
            Miljø = miljø;
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

        public Miljø Miljø { get; set; }

        public string ProxyHost { get; set; }

        /// <summary>
        ///     Angir schema ved bruk av proxy. Standardverdien er 'https'.
        /// </summary>
        public string ProxyScheme { get; set; } = "https";

        /// <summary>
        ///     Angir portnummeret som skal benyttes i forbindelse med bruk av proxy. Både ProxyHost og ProxyPort må spesifiseres
        ///     for at en proxy skal benyttes.
        /// </summary>
        public int ProxyPort { get; set; }

        /// <summary>
        ///     Angir timeout for komunikasjonen fra og til meldingsformindleren. Default tid er 30 sekunder.
        /// </summary>
        public int TimeoutIMillisekunder { get; set; } = 30000;

        /// <summary>
        ///     Sertifikat som bl.a. benyttes for å signere utgående meldinger. Må inneholde en privatnøkkel.
        /// </summary>
        public X509Certificate2 Avsendersertifikat { get; set; }

        /// <summary>
        ///     Organisasjonsnummeret til bedriften man skal sende på vegne av.
        ///     For informasjon om dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </summary>
        public string SendPåVegneAv { get; set; }

        public bool LoggForespørselOgRespons { get; set; } = false;

        public override string ToString()
        {
            return $"Miljø: {Miljø}, ProxyHost: {ProxyHost}, ProxyScheme: {ProxyScheme}, ProxyPort: {ProxyPort}, TimeoutIMillisekunder: {TimeoutIMillisekunder}, Avsendersertifikat: {Avsendersertifikat}, SendPåVegneAv: {SendPåVegneAv}, LoggForespørselOgRespons: {LoggForespørselOgRespons}";
        }
    }
}