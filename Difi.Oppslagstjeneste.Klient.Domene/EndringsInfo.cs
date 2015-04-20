using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Domene
{
    public abstract class EndringsInfo
    {
        /// <summary>
        /// Dato for når et objekt sist ble endret
        /// </summary>
        /// <remarks>
        /// SistOppdatert blir brukt i forskjellige objektet, i de objekter det er en del av definerer feltet når objektet sist ble endret. Det gir ingen informasjon om hvilken endring som da ble utført.
        /// </remarks>
        public DateTimeOffset? SistOppdatert { get; set; }

        /// <summary>
        /// Dato for når et objekt sist ble verifisert gyldig.
        /// </summary>
        /// <remarks>
        /// Feltet definerer når objektet sist ble verifisert/bekreftet. F.eks for mobiltelefonnummer betyr dette at en SMS er sendt til mobilnummeret og smsen er bekreftet mottatt av Person.
        /// </remarks>
        public DateTimeOffset? SistVerifisert { get; set; }

        public EndringsInfo(XmlElement element)
        {
            var sistVerifisert = element.Attributes["sistVerifisert", Navnerom.difi];
            if (sistVerifisert != null)
                SistVerifisert = DateTimeOffset.Parse(sistVerifisert.Value);

            var sistOppdatert = element.Attributes["sistOppdatert", Navnerom.difi];
            if (sistOppdatert != null)
                SistOppdatert = DateTimeOffset.Parse(sistOppdatert.Value);
        }
    }
}
