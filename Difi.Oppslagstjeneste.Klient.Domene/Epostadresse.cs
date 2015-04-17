using System.Diagnostics;
using System.Xml;
using Difi.Oppslagstjenesten.Domene;

namespace Difi.Oppslagstjenesten.Domene
{
    [DebuggerDisplay("Epost = {Epost}")]
    public class Epostadresse : EndringsInfo
    {
        /// <summary>
        /// Adresse til en elektronisk postkasse på Internett
        /// </summary>
        /// <remarks>
        /// •Ein tekststreng på formatet “^[\\w-\\](?:\\.[\\w-\\])*@(?:[\\w-]\.)[a-zA-Z]{2,7}$”
        /// •Klassifikasjon er Open Web Application Security Project (OWASP): Regulære uttrykk for validering
        /// </remarks>
        public string Epost { get; set; }

        public Epostadresse(XmlElement element)
            : base(element)
        {
            this.Epost = element.InnerText;
        }
    }
}
