using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
        private readonly OppslagstjenesteHelper _oppslagstjenesteHelper;

        /// <summary>
        ///     Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="oppslagstjenesteKonfigurasjon">
        ///     Konfigurasjon for oppslagstjeneste
        /// </param>
        public OppslagstjenesteKlient(OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            OppslagstjenesteKonfigurasjon = oppslagstjenesteKonfigurasjon;
            _oppslagstjenesteHelper = new OppslagstjenesteHelper(oppslagstjenesteKonfigurasjon);
        }

        public OppslagstjenesteKonfigurasjon OppslagstjenesteKonfigurasjon { get; }

        internal virtual OppslagstjenesteHelper GetClient()
        {
            return _oppslagstjenesteHelper;
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
        public EndringerSvar HentEndringer(long fraEndringsNummer, params Informasjonsbehov[] informasjonsbehov)
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
        public async Task<EndringerSvar> HentEndringerAsynkront(long fraEndringsNummer, params Informasjonsbehov[] informasjonsbehov)
        {
            var requestEnvelope = new EndringerEnvelope(OppslagstjenesteKonfigurasjon.Avsendersertifikat, OppslagstjenesteKonfigurasjon.SendPåVegneAv, fraEndringsNummer, informasjonsbehov);
            Logger.Log(TraceEventType.Verbose, requestEnvelope.XmlDocument.OuterXml);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentEndringerRespons>(requestEnvelope, responseDocument);
            return DtoConverter.ToDomainObject(dtoObject);
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
        public IEnumerable<Person> HentPersoner(string[] personidentifikator, params Informasjonsbehov[] informasjonsbehov)
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
        public async Task<IEnumerable<Person>> HentPersonerAsynkront(string[] personidentifikator, params Informasjonsbehov[] informasjonsbehov)
        {
            var requestEnvelope = new PersonerEnvelope(OppslagstjenesteKonfigurasjon.Avsendersertifikat, OppslagstjenesteKonfigurasjon.SendPåVegneAv, personidentifikator, informasjonsbehov);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentPersonerRespons>(requestEnvelope, responseDocument);
            var domainObject = DtoConverter.ToDomainObject(dtoObject);
            return domainObject.Personer;
        }

        private T ValidateAndConvertToDtoObject<T>(AbstractEnvelope requestEnvelope, ResponseContainer responseContainer)
        {
            Logger.Log(TraceEventType.Verbose, requestEnvelope.XmlDocument.OuterXml);
            ValidateResponse(requestEnvelope, responseContainer);
            Logger.Log(TraceEventType.Verbose, responseContainer.Envelope.InnerXml);
            return SerializeUtil.Deserialize<T>(responseContainer.BodyElement.InnerXml);
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
            var requestEnvelope = new PrintSertifikatEnvelope(OppslagstjenesteKonfigurasjon.Avsendersertifikat, OppslagstjenesteKonfigurasjon.SendPåVegneAv);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            var dtoObject = ValidateAndConvertToDtoObject<HentPrintSertifikatRespons>(requestEnvelope, responseDocument);
            return DtoConverter.ToDomainObject(dtoObject);
        }

        private void ValidateResponse(AbstractEnvelope envelope, ResponseContainer responseContainer)
        {
            var responsvalidator = new Oppslagstjenestevalidator(envelope.XmlDocument, responseContainer, OppslagstjenesteKonfigurasjon);
            responsvalidator.Valider();
        }
    }
}