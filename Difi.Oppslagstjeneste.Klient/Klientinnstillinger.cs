using System;
using System.Diagnostics;
using Difi.Felles.Utility;
using Difi.SikkerDigitalPost.Klient;

namespace Difi.Oppslagstjeneste.Klient
{
    public class Konfigurasjon : Klientkonfigurasjon
    {
        public Konfigurasjon(AbstraktMiljø miljø)
            :base(miljø)
        {
        }

        /// <summary>
        /// Eksponerer et grensesnitt for logging hvor brukere kan integrere sin egen loggefunksjonalitet eller en tredjepartsløsning som f.eks log4net. For bruk, angi en annonym funksjon med 
        /// følgende parametre: severity, konversasjonsid, metode, melding. Som default benyttes trace logging med navn 'Difi.Oppslagstjeneste.Klient' som kan aktiveres i applikasjonens konfigurasjonsfil. 
        /// </summary>
        public Action<TraceEventType, Guid?, string, string> Logger { get; set; }
    }
}
