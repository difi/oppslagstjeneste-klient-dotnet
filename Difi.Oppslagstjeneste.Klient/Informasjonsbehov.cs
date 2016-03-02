using System;

namespace Difi.Oppslagstjeneste.Klient
{
    /// <summary>
    ///     Beskriver det opplysningskrav som en Virksomhet har definert.
    /// </summary>
    [Flags]
    public enum Informasjonsbehov
    {
        /// <summary>
        ///     Person gir kun informasjon om Personen finnes i registeret og reservasjonsstatus. Person er implisitt og returneres
        ///     alltid.
        /// </summary>
        Person = 1,

        /// <summary>
        ///     Kontaktinfo gir informasjon om Person og Personers kontaktinformajon.
        /// </summary>
        Kontaktinfo = 2,

        /// <summary>
        ///     Sertifikat gir informasjon om Person sitt sertifikat som skal brukes i forbindelse med kryptering av Sikker Digital
        ///     Post.
        /// </summary>
        Sertifikat = 4,

        /// <summary>
        ///     SikkerDigitalPost gir informasjon om Person, postkasse og postkasseleverandøren.
        /// </summary>
        SikkerDigitalPost = 8,

        /// <summary>
        ///     Varslingsstatus gir en tekstlig beskrivelse av om bruker har utgått kontaktinformasjon eller ikke, ihht
        ///     eForvaltningsforskriftens §32 andre ledd.
        /// </summary>
        VarslingsStatus = 16
    }
}