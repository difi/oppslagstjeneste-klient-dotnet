using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient
{
    public class HentPrintSertifikatSvar
    {
        public HentPrintSertifikatSvar(XmlDocument xmlDocument)
        {
            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("env", Navnerom.SoapEnvelope);
            nsmgr.AddNamespace("ns", Navnerom.OppslagstjenesteDefinisjon);
            nsmgr.AddNamespace("difi", Navnerom.OppslagstjenesteMetadata);

            var personElements = xmlDocument.SelectSingleNode("/env:Envelope/env:Body/ns:HentPrintSertifikatRespons", nsmgr);

            this.PostkasseleverandørAdresse = personElements.SelectSingleNode("./ns:postkasseleverandoerAdresse", nsmgr).InnerText;
            this.Sertifikat = new X509Certificate2(Convert.FromBase64String(personElements.SelectSingleNode("./ns:X509Sertifikat", nsmgr).InnerText));
        }

        /// <summary>
        /// Et X509 Sertifikat.
        /// </summary>
        public X509Certificate2 Sertifikat { get; set; }

        /// <summary>
        /// Adresse til en leverandør av Postkassetjeneste
        /// </summary>
        public string PostkasseleverandørAdresse { get; set; }
    }
}
