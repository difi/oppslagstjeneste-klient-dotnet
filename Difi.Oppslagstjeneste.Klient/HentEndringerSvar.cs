using System.Collections.Generic;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient
{
    /// <summary>
    /// Response sendt fra Oppslagstjensten for å levere ut endringer fra kontakt og reservasjonsregisteret til Virksomhet
    /// </summary>
    public class HentEndringerSvar
    {
        /// <summary>
        /// Et endringsNummer, et nummer som identifiserer en endring i et register.
        /// </summary>
        /// <remarks>
        /// Enhver endring i Kontakt- og reservasjonsregisteret tilordnes ett løpenummer som øker med en (1) for hver endring. Dette brukes for at virksomheter som vedlikeholder 
        /// lokale kopier av registeret enkelt skal ha sporbarhet på alle endringer.
        /// </remarks>
        public long FraEndringsNummer { get; set; }

        /// <summary>
        /// Et endringsNummer, et nummer som identifiserer en endring i et register. Dersom TilEndringsNummer er ulik fra SenesteEndringsNummer så bør Offentlig Virksomhet sende ny HentEndringer forespørsel
        /// </summary>
        /// <remarks>
        /// Dersom TilEndringsNummer og SenesteEndringsNummer er lik finnes det ikke fler endringer i registeret som ikke er utlevert.
        /// </remarks>
        public long TilEndringsNummer { get; set; }

        /// <summary>
        /// Beskriver siste endringsnummer i et register. Kan sammenlignes med tilEndringsNummer for å vite om det finnes flere endringer i registeret.
        /// </summary>
        /// <remarks>
        /// Dersom TilEndringsNummer og SenesteEndringsNummer er lik finnes det ikke fler endringer i registeret som ikke er utlevert.
        /// </remarks>
        public long SenesteEndringsNummer { get; set; }

        /// <summary>
        /// Person er en Innbygger utlevert fra kontakt og reservasjonsregisteret.
        /// </summary>
        public IEnumerable<Person> Person { get; set; }

        public HentEndringerSvar()
        {
            this.Person = new List<Person>();
        }

        public static HentEndringerSvar FromDocument(XmlDocument xmlDocument)
        {
            var nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsmgr.AddNamespace("env", Navnerom.env11);
            nsmgr.AddNamespace("ns", Navnerom.OppslagstjenesteDefinisjon);
            nsmgr.AddNamespace("difi", Navnerom.OppslagstjenesteMetadata);
            
            var responseElement = xmlDocument.SelectSingleNode("/env:Envelope/env:Body/ns:HentEndringerRespons", nsmgr) as XmlElement;

            var response = new HentEndringerSvar
            {
                FraEndringsNummer = long.Parse(responseElement.Attributes["fraEndringsNummer"].Value),
                TilEndringsNummer = long.Parse(responseElement.Attributes["tilEndringsNummer"].Value),
                SenesteEndringsNummer = long.Parse(responseElement.Attributes["senesteEndringsNummer"].Value)
            };

            var personer = responseElement.SelectNodes("./difi:Person", nsmgr);

            var collection = response.Person as List<Person>;
            foreach (XmlElement item in personer)
            {
                collection.Add(new Person(item));
            }

            return response;
        }
    }
}
