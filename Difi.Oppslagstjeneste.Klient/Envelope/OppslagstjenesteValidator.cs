using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Felles.Utility;
using Difi.Felles.Utility.Exceptions;
using Difi.Felles.Utility.Security;
using Difi.Oppslagstjeneste.Klient.Security;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal class Oppslagstjenestevalidator : Responsvalidator
    {
        public Oppslagstjenestevalidator(XmlDocument sendtDokument, ResponseContainer responseContainer,
            OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
            : base(sendtDokument, responseContainer, oppslagstjenesteKonfigurasjon.Avsendersertifikat)
        {
            Miljø = oppslagstjenesteKonfigurasjon.Miljø;
        }

        public Miljø Miljø { get; set; }

        public void Valider()
        {
            var signedXmlWithAgnosticId = new SignedXmlWithAgnosticId(ResponseContainer.Envelope);
            signedXmlWithAgnosticId.LoadXml(ResponseContainer.HeaderSignatureElement);

            // Sørger for at motatt envelope inneholder signature confirmation og body samt at id'ne matcher mot header signatur
            ValiderSignaturreferanser(ResponseContainer.HeaderSignatureElement, signedXmlWithAgnosticId,
                new[] {"/env:Envelope/env:Header/wsse:Security/wsse11:SignatureConfirmation", "/env:Envelope/env:Body"});

            // Validerer SignatureConfirmation
            PerformSignatureConfirmation(ResponseContainer.HeaderSecurityElement);

            SjekkTimestamp(TimeSpan.FromSeconds(2000));

            ValiderResponssertifikat(signedXmlWithAgnosticId);
        }

        internal void ValiderResponssertifikat(SignedXmlWithAgnosticId signed)
        {
            var signatur = ResponseContainer.HeaderBinarySecurityToken.InnerText;
            var value = Convert.FromBase64String(signatur);
            var sertifikat = new X509Certificate2(value);

            var erGyldigSertifikatkjede = Miljø.Sertifikatkjedevalidator.ErGyldigSertifikatkjede(sertifikat);
            var erGyldigSertifikat = Sertifikatvalidator.ErGyldigSertifikat(sertifikat, "991825827");

            var erGodkjentSertifikat = erGyldigSertifikatkjede && erGyldigSertifikat;
            if (!erGodkjentSertifikat)
            {
                throw new SecurityException("Sertifikatet i responsen er ikke gyldig.");
            }

            var key = sertifikat.PublicKey.Key;
            if (!signed.CheckSignature(key))
                throw new SecurityException("Signaturen i motatt svar er ikke gyldig");
        }
    }
}