using System.Security.Cryptography.X509Certificates;

namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar
{
    /// <summary>
    ///     Svar fra Oppslagstjeneste som inneholder sertifikat til printleverandør og adressen til leverandøren
    ///     av postkassetjenesten.
    /// </summary>
    public class PrintSertifikatSvar
    {
        ///     <summary>
        ///         Et X509 Sertifikat.
        ///     </summary>
        public X509Certificate2 Printsertifikat { get; set; }

        /// <summary>
        ///     Adresse til en leverandør av Postkassetjeneste
        /// </summary>
        public string PostkasseleverandørAdresse { get; set; }
    }
}
