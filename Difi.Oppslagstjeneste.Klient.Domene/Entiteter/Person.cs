using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;

namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter
{
    [DebuggerDisplay("Personidentifikator = {Personidentifikator} Status = {Status}")]
    public class Person
    {
        
        
        public Person()
        {
            Reservasjon = false;
        }
        /// <summary>
        /// Varslingsstatus gir en tekstlig beskrivelse av om bruker har utgått kontaktinformasjon eller ikke, ihht eForvaltningsforskriftens §32 andre ledd.
        /// </summary>
        public Varslingsstatus Varslingsstatus { get; set; }

        /// <summary>
        ///     Identifikasjon av en person.
        /// </summary>
        /// <remarks>
        ///     personidentifikator er er enten et fødselsnummer et gyldig D-nummer.
        /// </remarks>
        public string Personidentifikator { get; set; }

        /// <summary>
        ///     Avgitt Forbehold i henhold til eForvaltningsforskriften. Standardverdi: NEI
        /// </summary>
        /// <remarks>
        ///     Reservasjon avgitt av Innbygger, brukt i henhold til eForvaltningsforskriften § 15 a.
        /// </remarks>
        public bool? Reservasjon { get; set; }

        /// <summary>
        ///     Status gir en tekstlig beskrivelse av tilstand.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        ///     Kontaktinformasjon er Adresse informasjon knyttet til en Person for å kommunisere med person.
        /// </summary>
        public Kontaktinformasjon Kontaktinformasjon { get; set; }

        /// <summary>
        ///     Adresse informasjon om Person sin Sikker DigitalPostKasse.
        /// </summary>
        /// <remarks>
        ///     SikkerDigitalPostAdresse er Innbygger sin adresse til Postkassen. Det inneholder nok informasjon til å adresse post
        ///     til Innbygger sin postkasse.
        /// </remarks>
        public SikkerDigitalPostAdresse SikkerDigitalPostAdresse { get; set; }

        /// <summary>
        ///     Et X509 Sertifikat.
        /// </summary>
        public X509Certificate2 X509Sertifikat { get; set; }
    }
}