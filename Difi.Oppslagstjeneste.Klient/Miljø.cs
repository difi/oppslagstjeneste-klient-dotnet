using System;
using System.Security.Cryptography.X509Certificates;
using Difi.Felles.Utility.Utilities;

namespace Difi.Oppslagstjeneste.Klient
{
    public class Miljø
    {
        internal Miljø(Uri url, X509Certificate2Collection godkjenteKjedeSertifikaterForRequest)
        {
            Url = url;
            GodkjenteKjedeSertifikaterForRequest = godkjenteKjedeSertifikaterForRequest;
            GodkjenteKjedeSertifikaterForRespons = CertificateChainUtility.ProduksjonsSertifikater();
        }

        public Uri Url { get; set; }

        public X509Certificate2Collection GodkjenteKjedeSertifikaterForRespons { get; set; }

        public X509Certificate2Collection GodkjenteKjedeSertifikaterForRequest { get; set; }

        /// <summary>
        ///     Verifikasjon 1 skal benyttes av kunder som skal integrere seg mot Difis felleskomponenter og kan også benyttes til
        ///     feilsøking i etablerte integrasjoner.
        ///     Verifikasjon 1 har samme versjon av Difis felleskomponenter som produksjonsløsningen.Når en ny versjon av Difis
        ///     felleskomponenter er satt i produksjon, vil verifikasjon 1 bli oppgradert tilsvarende.Ved oppgradering av
        ///     verifikasjon 1 til nye releaser, vil Difi kunne stenge tilgangen i en kortere periode (1-2 dager). Dette blir
        ///     varslet i forkant på samarbeidsportalen.
        /// </summary>
        public static Miljø FunksjoneltTestmiljøVerifikasjon1 => new Miljø(
            new Uri("https://kontaktinfo-ws-ver1.difi.no/kontaktinfo-external/ws-v5"),
            CertificateChainUtility.FunksjoneltTestmiljøSertifikater()
        );

        /// <summary>
        ///     Verifikasjon 2 blir brukt til testing av nye releaser av Difis felleskomponenter. Verifikasjon 2 kan benyttes til å
        ///     teste tjenester som er planlagt frem i tid. Ved oppgradering av verifikasjon 2 til nye releaser, vil Difi kunne
        ///     stenge tilgangen i en kortere periode (1-2 dager). Dette blir varslet i forkant på samarbeidsportalen.
        ///     Når Difis felleskomponenter oppgraderes, kan kunden teste sine eksisterende integrasjoner mot ny versjon i
        ///     verifikasjon 2 før den settes i produksjon.
        /// </summary>
        public static Miljø FunksjoneltTestmiljøVerifikasjon2 => new Miljø(
            new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v5"),
            CertificateChainUtility.FunksjoneltTestmiljøSertifikater()
        );

        public static Miljø Produksjonsmiljø => new Miljø(
            new Uri("https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v5"),
            CertificateChainUtility.ProduksjonsSertifikater()
        );
    }
}