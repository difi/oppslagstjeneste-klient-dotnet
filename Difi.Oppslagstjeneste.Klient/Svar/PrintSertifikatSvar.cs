using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.DTO;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    /// <summary>
    ///     Svar fra Oppslagstjeneste som inneholder sertifikat til printleverandør og adressen til leverandøren
    ///     av postkassetjenesten.
    /// </summary>
    public class PrintSertifikatSvar : Svar
    {
        public PrintSertifikatSvar(XmlDocument xmlDocument)
            : base(xmlDocument)
        {
        }

        public PrintSertifikatSvar()
        {
        }

        /// <summary>
        ///     <summary>
        ///         Et X509 Sertifikat.
        ///     </summary>
        public X509Certificate2 Printsertifikat { get; set; }

        /// <summary>
        ///     Adresse til en leverandør av Postkassetjeneste
        /// </summary>
        public string PostkasseleverandørAdresse { get; set; }

        protected override void ParseTilKlassemedlemmer()
        {
            var responseElement =
                XmlDocument.SelectSingleNode("/env:Envelope/env:Body", XmlNamespaceManager)
                    as XmlElement;
            var deserializedResponse = SerializeUtil.Deserialize<HentPrintSertifikatRespons>(responseElement.InnerXml);
            PostkasseleverandørAdresse = deserializedResponse.postkasseleverandoerAdresse;
            Printsertifikat = DtoKonverterer.TilDomeneObjekt(deserializedResponse.X509Sertifikat);
        }
    }
}