using System;
using System.Configuration;

namespace Difi.Oppslagstjeneste.Klient
{
    public abstract class Klientkonfigurasjon
    {
        protected abstract string Prefix { get; }

        /// <summary>
        /// Angir endepunktet for tjenesten. Denne verdien kan også overstyres i applikasjonens konfigurasjonsfil gjennom med appSettings verdi med nøkkelen '[Prefix]:ServiceUri'.
        /// </summary>
        public virtual Uri ServiceUri { get; set; }

        /// <summary>
        /// Angir host som skal benyttes i forbindelse med bruk av proxy. Både ProxyHost og ProxyPort må spesifiseres for at en proxy skal benyttes. Denne verdien kan også overstyres i 
        /// applikasjonens konfigurasjonsfil gjennom med appSettings verdi med nøkkelen '[Prefix]:ProxyHost'.
        /// </summary>
        public string ProxyHost { get; set; }

        /// <summary>
        /// Angir portnummeret som skal benyttes i forbindelse med bruk av proxy. Både ProxyHost og ProxyPort må spesifiseres for at en proxy skal benyttes. Denne verdien kan også overstyres i 
        /// applikasjonens konfigurasjonsfil gjennom med appSettings verdi med nøkkelen '[Prefix]:ProxyPort'.
        /// </summary>
        public int ProxyPort { get; set; }

        /// <summary>
        /// Angir schema ved bruk av proxy. Standardverdien er 'https'. Denne verdien kan også overstyres i 
        /// applikasjonens konfigurasjonsfil gjennom med appSettings verdi med nøkkelen '[Prefix]:ProxyScheme'.
        /// </summary>
        public string ProxyScheme { get; set; }

        /// <summary>
        /// Angir timeout for komunikasjonen fra og til meldingsformindleren. Default tid er 30 sekunder. Denne verdien kan også overstyres i 
        /// applikasjonens konfigurasjonsfil gjennom med appSettings verdi med nøkkelen '[Prefix]:TimeoutIMillisekunder'.
        /// </summary>
        public int TimeoutIMillisekunder { get; set; }

        /// <summary>
        /// Indikerer om proxy skal benyttes for oppkoblingen.
        /// </summary>
        public bool BrukProxy
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ProxyHost) && ProxyPort > 0;
            }
        }

        public Klientkonfigurasjon()
        {
            ProxyHost = SetFromAppConfig<string>(Prefix + ":ProxyHost", null);
            ProxyScheme = SetFromAppConfig<string>(Prefix + ":ProxyScheme", "https");
            TimeoutIMillisekunder = SetFromAppConfig<int>(Prefix + ":TimeoutIMillisekunder", (int)TimeSpan.FromSeconds(30).TotalMilliseconds);
        }

        protected T SetFromAppConfig<T>(string key, T @default)
        {
            var appSettings = ConfigurationManager.AppSettings;

            string value = appSettings[key];
            if (value == null)
                return @default;

            if (typeof(IConvertible).IsAssignableFrom(typeof(T)))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            else
            {
                return (T)Activator.CreateInstance(typeof(T), new object[] { value });
            }
        }

    }
}
