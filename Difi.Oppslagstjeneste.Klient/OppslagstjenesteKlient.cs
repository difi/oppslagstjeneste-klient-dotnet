using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ApiClientShared.Enums;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.XmlValidering;

namespace Difi.Oppslagstjeneste.Klient
{
    /// <summary>
    /// Oppslagstjenesten er et register over innbyggerens kontaktinformasjon og reservasjon, og er en fellesløsning som alle offentlige virksomheter skal bruke i sin tjenesteutvikling. Registeret gir tilgang til innbyggerens digitale kontaktinformasjon.
    /// </summary>
    public class OppslagstjenesteKlient
    {
        readonly OppslagstjenesteInstillinger _instillinger;
        readonly OppslagstjenesteKonfigurasjon _konfigurasjon;

        /// <summary>
        /// Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="avsendersertifikat">Brukes for å signere forespørselen mot Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet"/></param>
        /// <param name="valideringsSertifikat">Brukes for å validere svar fra Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet"/></param>
        public OppslagstjenesteKlient(X509Certificate2 avsendersertifikat, X509Certificate2 valideringsSertifikat, OppslagstjenesteKonfigurasjon konfigurasjon = null)
        {
            _instillinger = new OppslagstjenesteInstillinger
            {
                Avsendersertifikat = avsendersertifikat,
                Valideringssertifikat = valideringsSertifikat 
            };
            _konfigurasjon = konfigurasjon ?? new OppslagstjenesteKonfigurasjon();
        }
        
        /// <summary>
        /// Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="avsendersertifikatThumbprint">Thumbprint til sertifikat Virksomhet bruker til å signere 
        /// forespørselen. For informasjon om hvordan du finner dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet"/>.</param>
        /// <param name="valideringssertifikatThumbprint">Thumbprint til sertifikat Virksomhet bruker til å validere
        /// svar fra Oppslagstjenesten. For informasjon om hvordan du finner dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet"/></param>
        /// <param name="konfigurasjon"></param>
        public OppslagstjenesteKlient(string avsendersertifikatThumbprint, string valideringssertifikatThumbprint, OppslagstjenesteKonfigurasjon konfigurasjon = null)
        {
            _instillinger = new OppslagstjenesteInstillinger()
            {
                Avsendersertifikat =  ApiClientShared.CertificateUtility.SenderCertificate(avsendersertifikatThumbprint, Language.Norwegian),
                Valideringssertifikat = ApiClientShared.CertificateUtility.ReceiverCertificate(valideringssertifikatThumbprint, Language.Norwegian)
            };
            _konfigurasjon = konfigurasjon ?? new OppslagstjenesteKonfigurasjon();
        }

        /// <summary>
        /// Forespørsel sendt fra Virksomhet for å hente endringer fra Oppslagstjenesten.
        /// </summary>
        /// <param name="fraEndringsNummer">Brukes i endringsforespørsler for å hente alle endringer fra og med et bestemt endringsNummer.</param>
        /// <param name="informasjonsbehov">Beskriver det opplysningskrav som en Virksomhet har definert. Du kan angi fler behov f.eks Informasjonsbehov.Kontaktinfo | Informasjonsbehov.SikkerDigitalPost.</param>
        public EndringerSvar HentEndringer(long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
        {
            var envelope = new EndringerEnvelope(_instillinger, fraEndringsNummer, informasjonsbehov);
            var validator = SendEnvelope(envelope);
            validator.Valider();
            return new EndringerSvar(validator.ResponseDocument);
        }


        /// <summary>
        /// Forespørsel sendt fra Virksomhet for å hente Personer fra Oppslagstjenesten.
        /// </summary>
        /// <param name="personidentifikator">Identifikasjon av en person. Personidentifikator er er enten et fødselsnummer et gyldig D-nummer.</param>
        /// <param name="informasjonsbehov">Beskriver det opplysningskrav som en Virksomhet har definert. Du kan angi fler behov f.eks Informasjonsbehov.Kontaktinfo | Informasjonsbehov.SikkerDigitalPost.</param>
        public IEnumerable<Person> HentPersoner(string[] personidentifikator, Informasjonsbehov informasjonsbehov)
        {
            var envelope = new PersonerEnvelope(_instillinger, personidentifikator, informasjonsbehov);
            OppslagstjenesteValidator validator = SendEnvelope(envelope);
            validator.Valider();
            return new PersonerSvar(validator.ResponseDocument).Personer;
        }

        /// <summary>
        /// Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        /// Oppslagstjenesten.
        /// </summary>
        public PrintSertifikatSvar HentPrintSertifikat()
        {
            var envelope = new PrintSertifikatEnvelope(_instillinger);
            var validator = SendEnvelope(envelope);
            validator.Valider();
            return new PrintSertifikatSvar(validator.ResponseDocument);
        }

        private OppslagstjenesteValidator SendEnvelope(AbstractEnvelope envelope)
        {
            var request = (HttpWebRequest)WebRequest.Create(_konfigurasjon.ServiceUri);
            request.ContentType = "text/xml;charset=UTF-8";
            request.Headers.Add("SOAPAction", "\"\"");
            request.Method = "POST";
            request.KeepAlive = true;
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = _konfigurasjon.TimeoutIMillisekunder;
            if (_konfigurasjon.BrukProxy)
                request.Proxy = new WebProxy(new UriBuilder(_konfigurasjon.ProxyScheme, _konfigurasjon.ProxyHost, _konfigurasjon.ProxyPort).Uri);

            var xml = envelope.ToXml();
            var bytes = Encoding.UTF8.GetBytes(xml.OuterXml);

            var xmlValidator = new OppslagstjenesteXmlvalidator();
            var xmlValidert = xmlValidator.ValiderDokumentMotXsd(xml.OuterXml);
            if (!xmlValidert)
            {
                throw new XmlException(xmlValidator.ValideringsVarsler);
            }

            Logging.Log(TraceEventType.Verbose, xml.OuterXml);

            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            try
            {
                var response = request.GetResponse();
                var validator = new OppslagstjenesteValidator(response.GetResponseStream(), xml, (OppslagstjenesteInstillinger)envelope.Settings);
                return validator;
            }
            catch (WebException we)
            {
                using (var stream = we.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var error = reader.ReadToEnd();
                    var exception = new SoapException(error);
                    Logging.Log(TraceEventType.Critical, string.Format("> Feil ved sending (Skyldig: {0})", exception.Skyldig));
                    Logging.Log(TraceEventType.Critical, String.Format("  - {0}", exception.Beskrivelse));
                    throw exception;
                }
            }
        }
    }
}
