using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ApiClientShared;
using ApiClientShared.Enums;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
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
        private OppslagstjenesteHelper _oppslagstjenesteHelper;

        /// <summary>
        ///     Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="avsendersertifikat">
        ///     Brukes for å signere forespørselen mot Oppslagstjenesten. For informasjon om sertifikat, se online dokumentasjon:
        ///     <see cref="http://difi.github.io/oppslagstjeneste-klient-dotnet" />
        /// </param>
        /// <param name="oppslagstjenesteKonfigurasjon">
        ///     Konfigurasjon for oppslagstjeneste
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
        /// <param name="oppslagstjenesteKonfigurasjon">
        ///     Konfigurasjon for oppslagstjeneste
        /// </param>
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

        internal virtual OppslagstjenesteHelper GetClient()
        {
            if (_oppslagstjenesteHelper == null)
            {
                InitOppslagstjenesteHelper(OppslagstjenesteKonfigurasjon);
            }
            return _oppslagstjenesteHelper;
        }

        internal void InitOppslagstjenesteHelper(OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            _oppslagstjenesteHelper = new OppslagstjenesteHelper(oppslagstjenesteKonfigurasjon, OppslagstjenesteInstillinger);
        }

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
            return HentEndringerAsynkront(fraEndringsNummer, informasjonsbehov).Result;
        }

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
        public async Task<EndringerSvar> HentEndringerAsynkront(long fraEndringsNummer, Informasjonsbehov informasjonsbehov)
        {
            var requestEnvelope = new EndringerEnvelope(OppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.SendPåVegneAv, fraEndringsNummer, informasjonsbehov);
            Logger.Log(TraceEventType.Verbose, requestEnvelope.ToXml().OuterXml);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentEndringerRespons>(requestEnvelope, responseDocument);
            return DtoKonverterer.TilDomeneObjekt(dtoObject);
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
            return HentPersonerAsynkront(personidentifikator, informasjonsbehov).Result;
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
        public async Task<IEnumerable<Person>> HentPersonerAsynkront(string[] personidentifikator, Informasjonsbehov informasjonsbehov)
        {
            var requestEnvelope = new PersonerEnvelope(OppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.SendPåVegneAv, personidentifikator, informasjonsbehov);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentPersonerRespons>(requestEnvelope, responseDocument);
            var domainObject = DtoKonverterer.TilDomeneObjekt(dtoObject);
            return domainObject.Personer;
        }

        private T ValidateAndConvertToDtoObject<T>(AbstractEnvelope requestEnvelope, ResponseDokument responseDocument)
        {
            Logger.Log(TraceEventType.Verbose, requestEnvelope.ToXml().OuterXml);
            ValidateResponse(requestEnvelope, responseDocument);
            Logger.Log(TraceEventType.Verbose, responseDocument.Envelope.InnerXml);
            return SerializeUtil.Deserialize<T>(responseDocument.BodyElement.InnerXml);
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        ///     Oppslagstjenesten.
        /// </summary>
        public PrintSertifikatSvar HentPrintSertifikat()
        {
            return HentPrintSertifikatAsynkront().Result;
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        ///     Oppslagstjenesten.
        /// </summary>
        public async Task<PrintSertifikatSvar> HentPrintSertifikatAsynkront()
        {
            var requestEnvelope = new PrintSertifikatEnvelope(OppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.SendPåVegneAv);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentPrintSertifikatRespons>(requestEnvelope, responseDocument);
            return DtoKonverterer.TilDomeneObjekt(dtoObject);
        }

        private void ValidateResponse(AbstractEnvelope envelope, ResponseDokument responseDokument)
        {
            var responsvalidator = new Oppslagstjenestevalidator(envelope.ToXml(), responseDokument, OppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.Miljø as Miljø);
            responsvalidator.Valider();
        }
    }
}