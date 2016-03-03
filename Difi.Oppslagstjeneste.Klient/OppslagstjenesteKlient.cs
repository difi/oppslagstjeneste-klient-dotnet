using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ApiClientShared;
using ApiClientShared.Enums;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.XmlValidering;
using Person = Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Person;

namespace Difi.Oppslagstjeneste.Klient
{
    /// <summary>
    ///     Oppslagstjenesten er et register over innbyggerens kontaktinformasjon og reservasjon, og er en fellesløsning som
    ///     alle offentlige virksomheter skal bruke i sin tjenesteutvikling. Registeret gir tilgang til innbyggerens digitale
    ///     kontaktinformasjon.
    /// </summary>
    public class OppslagstjenesteKlient
    {
        /// <summary>
        ///     Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="avsendersertifikat">
        ///     Brukes for å signere forespørselen mot Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        /// <param name="valideringsSertifikat">
        ///     Brukes for å validere svar fra Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        public OppslagstjenesteKlient(X509Certificate2 avsendersertifikat,
            OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            OppslagstjenesteInstillinger = new OppslagstjenesteInstillinger
            {
                Avsendersertifikat = avsendersertifikat
            };
            OppslagstjenesteKonfigurasjon = oppslagstjenesteKonfigurasjon;
        }

        /// <summary>
        ///     Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="avsendersertifikatThumbprint">
        ///     Thumbprint til sertifikat Virksomhet bruker til å signere
        ///     forespørselen. For informasjon om hvordan du finner dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />.
        /// </param>
        /// <param name="valideringssertifikatThumbprint">
        ///     Thumbprint til sertifikat Virksomhet bruker til å validere
        ///     svar fra Oppslagstjenesten. For informasjon om hvordan du finner dette, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        /// <param name="oppslagstjenesteKonfigurasjon"></param>
        public OppslagstjenesteKlient(string avsendersertifikatThumbprint,
            OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
            :
                this(
                CertificateUtility.SenderCertificate(avsendersertifikatThumbprint, Language.Norwegian),
                oppslagstjenesteKonfigurasjon)
        {
        }

        public OppslagstjenesteInstillinger OppslagstjenesteInstillinger { get; }

        public OppslagstjenesteKonfigurasjon OppslagstjenesteKonfigurasjon { get; }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente endringer fra Oppslagstjenesten.
        /// </summary>
        /// <param name="fraEndringsNummer">
        ///     Brukes i endringsforespørsler for å hente alle endringer fra og med et bestemt
        ///     endringsNummer.
        /// </param>
        /// <param name="informasjonsbehov">
        ///     Beskriver det opplysningskrav som en Virksomhet har definert. Du kan angi fler behov
        ///     f.eks Informasjonsbehov.Kontaktinfo | Informasjonsbehov.SikkerDigitalPost.
        /// </param>
        public EndringerSvar HentEndringer(long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
        {
            var envelope = new EndringerEnvelope(OppslagstjenesteInstillinger, fraEndringsNummer, informasjonsbehov);
            var validator = SendEnvelope(envelope);
            validator.Valider();
            return new EndringerSvar(validator.MottattDokument);
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Personer fra Oppslagstjenesten.
        /// </summary>
        /// <param name="personidentifikator">
        ///     Identifikasjon av en person. Personidentifikator er er enten et fødselsnummer et
        ///     gyldig D-nummer.
        /// </param>
        /// <param name="informasjonsbehov">
        ///     Beskriver det opplysningskrav som en Virksomhet har definert. Du kan angi fler behov
        ///     f.eks Informasjonsbehov.Kontaktinfo | Informasjonsbehov.SikkerDigitalPost.
        /// </param>
        public IEnumerable<Person> HentPersoner(string[] personidentifikator, Informasjonsbehov informasjonsbehov)
        {
            var envelope = new PersonerEnvelope(OppslagstjenesteInstillinger, personidentifikator, informasjonsbehov);
            var validator = SendEnvelope(envelope);
            validator.Valider();
            
            return new PersonerSvar(validator.MottattDokument).Personer;
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        ///     Oppslagstjenesten.
        /// </summary>
        public PrintSertifikatSvar HentPrintSertifikat()
        {
            var envelope = new PrintSertifikatEnvelope(OppslagstjenesteInstillinger);
            var validator = SendEnvelope(envelope);
            validator.Valider();
            return new PrintSertifikatSvar(validator.MottattDokument);
        }

        private Oppslagstjenestevalidator SendEnvelope(AbstractEnvelope envelope)
        {
            var request = (HttpWebRequest) WebRequest.Create(OppslagstjenesteKonfigurasjon.Miljø.Url);
            request.ContentType = "application/soap+xml";
            request.Headers.Add("SOAPAction", "\"\"");
            request.Method = "POST";
            request.KeepAlive = true;
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = OppslagstjenesteKonfigurasjon.TimeoutIMillisekunder;

            var netVersion = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name == "System.Core").First().Version.ToString();

            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

            request.UserAgent = string.Format("DifiOppslagstjeneste/{1} .NET/{0},", netVersion, assemblyVersion);

            if (OppslagstjenesteKonfigurasjon.BrukProxy)
                request.Proxy =
                    new WebProxy(
                        new UriBuilder(OppslagstjenesteKonfigurasjon.ProxyScheme,
                            OppslagstjenesteKonfigurasjon.ProxyHost, OppslagstjenesteKonfigurasjon.ProxyPort).Uri);

            var xml = envelope.ToXml();
            var bytes = Encoding.UTF8.GetBytes(xml.OuterXml);

            var xmlValidator = new OppslagstjenesteXmlvalidator();
            var xmlValidert = xmlValidator.ValiderDokumentMotXsd(xml.OuterXml);
            if (!xmlValidert)
            {
                throw new XmlException(xmlValidator.ValideringsVarsler);
            }

            Logger.Log(TraceEventType.Verbose, xml.OuterXml);

            try
            {
                var requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw new SendException(
                    "Får ikke kontakt med Oppslagstjenesten. Sjekk tilkoblingsdetaljer og prøv på nytt.");
            }
            try
            {
                var response = request.GetResponse();

                var responsdokument = new XmlDocument();
                responsdokument.Load(response.GetResponseStream());

                var validator = new Oppslagstjenestevalidator(xml, responsdokument,
                    (OppslagstjenesteInstillinger) envelope.Settings, (Miljø) OppslagstjenesteKonfigurasjon.Miljø);
                return validator;
            }
            catch (WebException we)
            {
                using (var stream = we.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var error = reader.ReadToEnd();
                    var exception = new SoapException(error);
                    Logger.Log(TraceEventType.Critical,
                        string.Format("> Feil ved sending (Skyldig: {0})", exception.Skyldig));
                    Logger.Log(TraceEventType.Critical, string.Format("  - {0}", exception.Beskrivelse));
                    throw exception;
                }
            }
        }
    }
}