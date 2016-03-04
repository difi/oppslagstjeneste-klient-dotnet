namespace Difi.Oppslagstjeneste.Klient.Domene.Enums
{
    /// <summary>
    ///     Varslingsstatus gir en tekstlig beskrivelse av om bruker har utgått kontaktinformasjon eller ikke, ihht
    ///     eForvaltningsforskriftens §32 andre ledd.
    /// </summary>
    public enum Varslingsstatus
    {
        /// <remarks>Person har utgått kontaktinformasjon, er reservert, er slettet eller finnes ikke i registeret</remarks>
        KAN_IKKE_VARSLES,

        /// <remarks>Person har ikke utgått kontaktinformasjon</remarks>
        KAN_VARSLES
    }
}