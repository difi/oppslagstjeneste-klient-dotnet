using System;
using System.Diagnostics;
using Difi.Oppslagstjeneste.Klient.Felles;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon : Klientkonfigurasjon
    {
        protected override string Prefix
        {
            get { return "Oppslagstjeneste"; }
        }
                
        public OppslagstjenesteKonfigurasjon()
        {
            ServiceUri = SetFromAppConfig<Uri>(Prefix + ":ServiceUri", new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4"));
            Logger = Logging.ConsoleLogger();
        }

        /// <summary>
        /// Eksponerer et grensesnitt for logging hvor brukere kan integrere sin egen loggefunksjonalitet eller en tredjepartsløsning som f.eks log4net. For bruk, angi en annonym funksjon med 
        /// følgende parametre: severity, konversasjonsid, metode, melding. Som default benyttes trace logging med navn 'Difi.Oppslagstjeneste.Klient' som kan aktiveres i applikasjonens konfigurasjonsfil. 
        /// </summary>
        public Action<TraceEventType, Guid?, string, string> Logger { get; set; }
    }
}
