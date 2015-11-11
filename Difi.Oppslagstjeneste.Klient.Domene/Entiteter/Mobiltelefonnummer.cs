using System.Diagnostics;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter
{
    /// <summary>
    /// Informasjon om en Person sitt Mobiltelefonnummer registrert i kontakt og reservasjonsregisteret
    /// </summary>
    [DebuggerDisplay("Nummer = {Nummer}")]
    public class Mobiltelefonnummer : EndringsInfo
    {
        /// <summary>
        /// Et internasjonalt mobiltelefonnummer.
        /// </summary>
        /// <remarks>
        /// Et mobiltelefonnummer lagret i Digital kontaktregister har følgende format:
        /// •En tekststreng på formatet: "^\\+?[- _0-9]+$" 
        /// •Minimum lengde : 8 
        /// •Maximum lengde : 20 
        /// •Tillatte tegn: 0-9 og eventuelt + som første karakter 
        /// 
        ///  I tillegg er det en ekstra validering av norske mobilnummer. Norske mobilnummer er definert som: 
        ///  •nummeret starter på 0047,+47 eller er på 8 tegn 
        ///  
        /// For disse mobiltelefonnummer er det følgende validering: 
        /// •første siffer (etter evt. landkode) er 9 eller 4 
        /// 
        /// Dette er i henhold til Nummerplan E.164
        /// </remarks>
        public string Nummer { get; set; }

        public Mobiltelefonnummer(XmlElement element) : base(element)
        {
            Nummer = element.InnerText;
        }
    }
}
