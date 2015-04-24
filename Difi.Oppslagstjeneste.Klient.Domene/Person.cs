using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Domene
{
    [DebuggerDisplay("Personidentifikator = {Personidentifikator} Status = {Status}")]
    public class Person
    {
        /// <summary>
        /// Identifikasjon av en person.
        /// </summary>
        /// <remarks>
        /// personidentifikator er er enten et fødselsnummer et gyldig D-nummer.
        /// </remarks>
        public string Personidentifikator { get; set; }

        /// <summary>
        /// Avgitt Forbehold i henhold til eForvaltningsforskriften. Standardverdi: NEI
        /// </summary>
        /// <remarks>
        /// Reservasjon avgitt av Innbygger, brukt i henhold til eForvaltningsforskriften § 15 a.
        /// </remarks>
        public bool? Reservasjon { get; set; }

        /// <summary>
        /// Status gir en tekstlig beskrivelse av tilstand.
        /// </summary>
        public Tilstand Status { get; set; }

        /// <summary>
        /// Kontaktinformasjon er Adresse informasjon knyttet til en Person for å kommunisere med person.
        /// </summary>
        public Kontaktinformasjon Kontaktinformasjon { get; set; }

        /// <summary>
        /// Adresse informasjon om Person sin Sikker DigitalPostKasse.
        /// </summary>
        /// <remarks>
        /// SikkerDigitalPostAdresse er Innbygger sin adresse til Postkassen. Det inneholder nok informasjon til å adresse post til Innbygger sin postkasse.
        /// </remarks>
        public Sikkerdigitalpostadresse Sikkerdigitalpostadresse { get; set; }

        /// <summary>
        /// Et X509 Sertifikat.
        /// </summary>
        public X509Certificate2 X509sertifikat { get; set; }

        public Person()
        {
            this.Reservasjon = false;
        }

        public Person(System.Xml.XmlElement item)
        {
            this.Personidentifikator = item["personidentifikator", Navnerom.OppslagstjenesteMetadata].InnerText;

            var reservasjon = item["reservasjon", Navnerom.OppslagstjenesteMetadata];
            if (reservasjon != null)
            {
                if (reservasjon.InnerText == "NEI")
                    this.Reservasjon = false;
                else
                    this.Reservasjon = true;
            }

            var status = item["status", Navnerom.OppslagstjenesteMetadata];
            if (status != null)
                this.Status = (Tilstand)Enum.Parse(typeof(Tilstand), status.InnerText);

            var kontaktinformasjon = item["Kontaktinformasjon", Navnerom.OppslagstjenesteMetadata];
            if (kontaktinformasjon != null)
                this.Kontaktinformasjon = new Kontaktinformasjon(kontaktinformasjon);

            var sikkerDigitalPostAdresse = item["SikkerDigitalPostAdresse", Navnerom.OppslagstjenesteMetadata];
            if (sikkerDigitalPostAdresse != null)
                this.Sikkerdigitalpostadresse = new Sikkerdigitalpostadresse(sikkerDigitalPostAdresse);

            var X509Certificate = item["X509Sertifikat", Navnerom.OppslagstjenesteMetadata];
            if (X509Certificate != null)
            {
                X509sertifikat = new X509Certificate2(System.Convert.FromBase64String(X509Certificate.InnerText));
            }

        }
    }
}
