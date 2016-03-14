namespace Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums
{
    /// <summary>
    ///     Beskriver det opplysningskrav som en Virksomhet har definert.
    /// </summary>
    public enum Informasjonsbehov
    {
        /// <summary>
        ///     Person gir kun informasjon om Personen finnes i registeret og reservasjonsstatus. Person er implisitt og returneres
        ///     alltid.
        /// </summary>
        Person,

        /// <summary>
        ///     Kontaktinfo gir informasjon om Person og Personers kontaktinformajon.
        /// </summary>
        Kontaktinfo,

        /// <summary>
        ///     Sertifikat gir informasjon om Person sitt sertifikat som skal brukes i forbindelse med kryptering av Sikker Digital
        ///     Post.
        /// </summary>
        Sertifikat,

        /// <summary>
        ///     SikkerDigitalPost gir informasjon om Person, postkasse og postkasseleverandøren.
        /// </summary>
        SikkerDigitalPost,

        /// <summary>
        ///     Varslingsstatus gir en tekstlig beskrivelse av om bruker har utgått kontaktinformasjon eller ikke, ihht
        ///     eForvaltningsforskriftens §32 andre ledd.
        /// </summary>
        VarslingsStatus
    }
}