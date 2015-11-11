using System;
using System.IO;
using System.Security;
using System.Xml;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Enums;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class OppslagstjenesteValidator : Responsvalidator
    {
        OppslagstjenesteInstillinger instillinger;
        
        public OppslagstjenesteValidator(XmlDocument responsdokument, XmlDocument sentEnvelope, OppslagstjenesteInstillinger instillinger)
            : base(responsdokument, SoapVersion.Soap11, sentEnvelope, instillinger.Avsendersertifikat)
        {
            this.instillinger = instillinger;
      
        }

        public void Valider()
        {
            var signed = new SignedXmlWithAgnosticId(ResponseDocument);
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
            if (!signed.CheckSignature(instillinger.Valideringssertifikat.PublicKey.Key))
                throw new Exception("Signaturen i motatt svar er ikke gyldig");

            //var erGyldigSertifikat = Sertifikatkjedevalidator.ErGyldigResponssertifikat(_sertifikat);

            //if (!erGyldigSertifikat)
            //{
            //    throw new SecurityException("Sertifikatet som er angitt i signaturen er ikke en del av en gyldig sertifikatkjede.");
            //}
        }
    }
}
