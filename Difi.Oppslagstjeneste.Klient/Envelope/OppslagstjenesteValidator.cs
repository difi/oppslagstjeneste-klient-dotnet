using System;
using System.IO;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Felles.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class OppslagstjenesteValidator : Responsvalidator
    {
        OppslagstjenesteInstillinger instillinger;

        public OppslagstjenesteValidator(Stream response, XmlDocument sentEnvelope, OppslagstjenesteInstillinger instillinger)
            : base(response, SoapVersion.Soap11, sentEnvelope, instillinger.Avsendersertifikat)
        {
            this.instillinger = instillinger;
        }

        public void Valider()
        {
            var signed = new SignedXmlWithAgnosticId(ResponseDocument);
            signed.LoadXml(HeaderSignatureElement);

            // Sørger for at motatt envelope inneholder signature confirmation og body samt at id'ne matcher mot header signatur
            ValiderSignaturreferanser(HeaderSignatureElement, signed, new string[] { "/env:Envelope/env:Header/wsse:Security/wsse11:SignatureConfirmation", "/env:Envelope/env:Body" });

            // Validerer SignatureConfirmation
            PerformSignatureConfirmation(HeaderSecurityElement);

            SjekkTimestamp(TimeSpan.FromSeconds(2000));

            // Sjekker signatur
            if (!signed.CheckSignature(instillinger.Valideringssertifikat.PublicKey.Key))
                throw new Exception("Signaturen i motatt svar er ikke gyldig");
        }
    }
}
