using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient
{
    /// <summary>
    /// Svar fra Oppslagstjeneste som inneholder sertifikat til printleverandør og adressen til leverandøren
    /// av postkassetjenesten.
    /// </summary>
    public class HentPrintSertifikatSvar
    {
        private readonly XmlNamespaceManager _namespaceManager;

        public HentPrintSertifikatSvar(XmlDocument xmlDocument)
        {
            _namespaceManager = InitalizeNamespaceManager(xmlDocument);
            ParseToClassMembers(xmlDocument);
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

        private XmlNamespaceManager InitalizeNamespaceManager(XmlDocument xmlDocument)
        {
            var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("env", Navnerom.env11);
            namespaceManager.AddNamespace("ns", Navnerom.krr);
            namespaceManager.AddNamespace("difi", Navnerom.difi);
            return namespaceManager;
        }

        private void ParseToClassMembers(XmlDocument xmlDocument)
        {
            try
            {
                var personElements = xmlDocument.SelectSingleNode(
                    "/env:Envelope/env:Body/ns:HentPrintSertifikatRespons", _namespaceManager);

                PostkasseleverandørAdresse =
                    personElements.SelectSingleNode("./ns:postkasseleverandoerAdresse", _namespaceManager).InnerText;
                Printsertifikat = new X509Certificate2(
                    Convert.FromBase64String(
                        personElements.SelectSingleNode("./ns:X509Sertifikat", _namespaceManager).InnerText));
            }
            catch (Exception e)
            {
                throw new XmlParseException("Klarte ikke å parse svar fra Oppslagstjenesten.", e);
            }
        }
    }
}
