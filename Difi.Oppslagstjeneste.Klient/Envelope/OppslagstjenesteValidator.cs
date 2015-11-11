using System;
using System.Xml;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class Oppslagstjenestevalidator : Responsvalidator
    {
        public OppslagstjenesteInstillinger OppslagstjenesteInstillinger { get; }
        
        public Oppslagstjenestevalidator(XmlDocument mottattDokument, XmlDocument sendtDokument, OppslagstjenesteInstillinger oppslagstjenesteInstillinger)
            : base(mottattDokument, SoapVersion.Soap11, sendtDokument, oppslagstjenesteInstillinger.Avsendersertifikat)
        {
            OppslagstjenesteInstillinger = oppslagstjenesteInstillinger;
        }

        public void Valider()
        {
            var signed = new SignedXmlWithAgnosticId(MottattDokument);
            signed.LoadXml(HeaderSignatureElement);

            // Sørger for at motatt envelope inneholder signature confirmation og body samt at id'ne matcher mot header signatur
            ValiderSignaturreferanser(HeaderSignatureElement, signed, new[] { "/env:Envelope/env:Header/wsse:Security/wsse11:SignatureConfirmation", "/env:Envelope/env:Body" });

            // Validerer SignatureConfirmation
            PerformSignatureConfirmation(HeaderSecurityElement);

            SjekkTimestamp(TimeSpan.FromSeconds(2000));

            ValiderResponssertifikat(signed);
        }

        private void ValiderResponssertifikat(SignedXmlWithAgnosticId signed)
        {
            // Sjekker signatur
            if (!signed.CheckSignature(OppslagstjenesteInstillinger.Valideringssertifikat.PublicKey.Key))
                throw new Exception("Signaturen i motatt svar er ikke gyldig");

            //var erGyldigSertifikat = Sertifikatkjedevalidator.ErGyldigResponssertifikat(_sertifikat);

            //if (!erGyldigSertifikat)
            //{
            //    throw new SecurityException("Sertifikatet som er angitt i signaturen er ikke en del av en gyldig sertifikatkjede.");
            //}
        }
    }
}
