namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter
{
    public class Epostadresse : EndringsInfo
    {
        /// <summary>
        ///     Adresse til en elektronisk postkasse på Internett
        /// </summary>
        /// <remarks>
        ///     •En tekststreng på formatet “^[\\w-\\](?:\\.[\\w-\\])*@(?:[\\w-]\.)[a-zA-Z]{2,7}$”
        ///     •Klassifikasjon er Open Web Application Security Project (OWASP): Regulære uttrykk for validering
        /// </remarks>
        public string Epost { get; set; }
    }
}