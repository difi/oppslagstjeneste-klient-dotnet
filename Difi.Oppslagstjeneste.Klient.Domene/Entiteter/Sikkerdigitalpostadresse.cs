using System.Diagnostics;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter
{
    /// <summary>
    ///     Adresse informasjon om Person sin Sikker DigitalPostKasse.
    /// </summary>
    public class SikkerDigitalPostAdresse
    {
        /// <summary>
        ///     Adresse til en Innbygger sin Postkasse hos en Postkasseleverandør
        /// </summary>
        /// <remarks>
        ///     Dette er en unik adresse for en Person sin Postkasseadresse hos en Postkasseleverandør. Enten digipost eller eboks.
        ///     For definisjon av postkasseadressen hos Digipost se: https://www.digipost.no/plattform/felles/digipostadresser
        /// </remarks>
        public string Postkasseadresse { get; set; }

        /// <summary>
        ///     Adresse til en leverandør av Postkassetjeneste
        /// </summary>
        /// <remarks>
        ///     Dette er en unik adresse for en leverandør, enten en Postkasseleverandør eller Utskrifts og forsendelsestjeneeste
        ///     leverandør.
        ///     Adressen brukt for adressering er Organisasjonsnummeret for leverandører
        /// </remarks>
        public string PostkasseleverandørAdresse { get; set; }
    }
}