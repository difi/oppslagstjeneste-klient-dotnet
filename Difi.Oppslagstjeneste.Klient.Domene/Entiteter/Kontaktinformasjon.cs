using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter
{
    /// <summary>
    ///     Kontaktinformasjon er Adresse informasjon knyttet til en Person for å kommunisere med person
    /// </summary>
    public class Kontaktinformasjon
    {
        /// <summary>
        ///     Informasjon om en Person sitt Mobiltelefonnummer registrert i kontakt og reservasjonsregisteret.
        /// </summary>
        public Mobiltelefonnummer Mobiltelefonnummer { get; set; }

        /// <summary>
        ///     Informasjon om en Person sitt Epostadresse registrert i kontakt og reservasjonsregisteret.
        /// </summary>
        public Epostadresse Epostadresse { get; set; }
    }
}