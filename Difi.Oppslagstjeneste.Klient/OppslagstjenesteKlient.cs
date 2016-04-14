using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Svar;
using Difi.Oppslagstjeneste.Klient.DTO;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;
using log4net;
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
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog RequestLog = LogManager.GetLogger($"{typeof (OppslagstjenesteKlient).Namespace}.RequestLog");
        private readonly OppslagstjenesteHelper _oppslagstjenesteHelper;

        /// <summary>
        ///     Oppslagstjenesten for kontakt og reservasjonsregisteret.
        /// </summary>
        /// <param name="oppslagstjenesteKonfigurasjon">
        ///     Konfigurasjon for oppslagstjeneste
        /// </param>
        public OppslagstjenesteKlient(OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
        {
            Log.Debug(oppslagstjenesteKonfigurasjon.ToString());
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
            try
            {
                return HentEndringerAsynkront(fraEndringsNummer, informasjonsbehov).Result;
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                throw;
            }
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

            Log.Debug($"HentEndringerAsynkront(fraEndringsNummer:{fraEndringsNummer} , informasjonsbehov:{informasjonsbehov})");
            RequestLog.Debug(requestEnvelope.XmlDocument.OuterXml);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);

            RequestLog.Debug(responseDocument.Envelope.InnerXml);
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
            try
            {
                return HentPersonerAsynkront(personidentifikator, informasjonsbehov).Result;
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                throw;
            }
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
            var requestEnvelope = new PersonsEnvelope(OppslagstjenesteKonfigurasjon.Avsendersertifikat, OppslagstjenesteKonfigurasjon.SendPåVegneAv, personidentifikator, informasjonsbehov);
            Log.Debug($"HentPersonerAsynkront(personidentifikator:{personidentifikator} , informasjonsbehov:{informasjonsbehov})");
            RequestLog.Debug(requestEnvelope.XmlDocument.OuterXml);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            RequestLog.Debug(responseDocument.Envelope.InnerXml);
            var dtoObject = ValidateAndConvertToDtoObject<HentPersonerRespons>(requestEnvelope, responseDocument);
            var domainObject = DtoConverter.ToDomainObject(dtoObject);
            return domainObject.Personer;
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        ///     Oppslagstjenesten.
        /// </summary>
        public PrintSertifikatSvar HentPrintSertifikat()
        {
            try
            {
                return HentPrintSertifikatAsynkront().Result;
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                throw;
            }
        }

        /// <summary>
        ///     Forespørsel sendt fra Virksomhet for å hente Sertifikater fra Printleverandør i Sikker Digital Post fra
        ///     Oppslagstjenesten.
        /// </summary>
        public async Task<PrintSertifikatSvar> HentPrintSertifikatAsynkront()
        {
            var requestEnvelope = new PrintCertificateEnvelope(OppslagstjenesteKonfigurasjon.Avsendersertifikat, OppslagstjenesteKonfigurasjon.SendPåVegneAv);
            Log.Debug($"HentPrintSertifikatAsynkront");
            RequestLog.Debug(requestEnvelope.XmlDocument.OuterXml);
            var responseDocument = await GetClient().SendAsync(requestEnvelope);
            RequestLog.Debug(responseDocument.Envelope.InnerXml);
            var dtoObject = ValidateAndConvertToDtoObject<HentPrintSertifikatRespons>(requestEnvelope, responseDocument);
            return DtoConverter.ToDomainObject(dtoObject);
        }

        private T ValidateAndConvertToDtoObject<T>(AbstractEnvelope requestEnvelope, ResponseContainer responseContainer)
        {
            ValidateResponse(requestEnvelope, responseContainer);
            return SerializeUtil.Deserialize<T>(responseContainer.BodyElement.InnerXml);
        }

        private void ValidateResponse(AbstractEnvelope envelope, ResponseContainer responseContainer)
        {
            var responsvalidator = new OppslagstjenesteValidator(envelope.XmlDocument, responseContainer, OppslagstjenesteKonfigurasjon);
            responsvalidator.Validate();
        }
    }
}