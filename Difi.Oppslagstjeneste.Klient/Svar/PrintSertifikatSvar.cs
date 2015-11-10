using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;

namespace Difi.Oppslagstjeneste.Klient.Svar
{
    /// <summary>
    /// Svar fra Oppslagstjeneste som inneholder sertifikat til printleverandør og adressen til leverandøren
    /// av postkassetjenesten.
    /// </summary>
    public class PrintSertifikatSvar : Svar
    {
        public PrintSertifikatSvar(XmlDocument xmlDocument) : base(xmlDocument)
        {
        }
        
        /// <summary>
        /// <summary>
        /// Et X509 Sertifikat.
        /// </summary>
        public X509Certificate2 Printsertifikat { get; set; }

        /// <summary>
        /// Adresse til en leverandør av Postkassetjeneste
        /// </summary>
        public string PostkasseleverandørAdresse { get; set; }

        protected override void ParseTilKlassemedlemmer()
        {
            try
            {
                var personElements = XmlDocument.SelectSingleNode(
                    "/env:Envelope/env:Body/ns:HentPrintSertifikatRespons", XmlNamespaceManager);

                PostkasseleverandørAdresse =
                    personElements.SelectSingleNode("./ns:postkasseleverandoerAdresse", XmlNamespaceManager).InnerText;
                Printsertifikat = new X509Certificate2(
                    Convert.FromBase64String(
                        personElements.SelectSingleNode("./ns:X509Sertifikat", XmlNamespaceManager).InnerText));
            }
            catch (Exception e)
            {
                throw new XmlParseException("Klarte ikke å parse svar fra Oppslagstjenesten.", e);
            }
        }
    }
}
