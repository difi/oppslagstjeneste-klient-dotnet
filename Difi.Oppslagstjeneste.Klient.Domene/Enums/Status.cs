namespace Difi.Oppslagstjeneste.Klient.Domene.Enums
{
    /// <summary>
    ///     For oppslag av Person(er) er alle statuskoder relevante. For oppslag på endringer er kun status AKTIV og SLETTET
    ///     relevant.
    /// </summary>
    public enum Status
    {
        /// <summary>
        ///     Person finnes i registeret.
        /// </summary>
        /// <remarks>
        ///     Ved AKTIV levers alle felter på Person ut, samt den øvrige informasjonen spesifisert i informasjonsbehovet dersom
        ///     denne informasjonen finnes i registeret.
        /// </remarks>
        AKTIV,

        /// <summary>
        ///     Person er slettet fra registeret.
        /// </summary>
        /// <remarks>
        ///     Ved SLETTET og IKKE_REGISTRERT har ikke registeret informasjon om Kontaktinfo eller PostkasseInfo eller Sertifikat,
        ///     og vil ikke kunne levere ut disse elementene selv om
        ///     det er spesifisert i informasjonsbehov fra Offentlig virksomhet.
        /// </remarks>
        SLETTET,

        /// <summary>
        ///     Person finnes ikke i registeret.
        /// </summary>
        /// <remarks>
        ///     Ved SLETTET og IKKE_REGISTRERT har ikke registeret informasjon om Kontaktinfo eller PostkasseInfo eller Sertifikat,
        ///     og vil ikke kunne levere ut disse elementene selv om
        ///     det er spesifisert i informasjonsbehov fra Offentlig virksomhet.
        /// </remarks>
        IKKE_REGISTRERT
    }
}