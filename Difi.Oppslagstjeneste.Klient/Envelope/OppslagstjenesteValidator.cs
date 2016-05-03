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
    internal class OppslagstjenesteValidator : ResponseValidator
    {
        public OppslagstjenesteValidator(XmlDocument sentDocument, ResponseContainer responseContainer,
            OppslagstjenesteKonfigurasjon oppslagstjenesteConfiguration)
            : base(sentDocument, responseContainer, oppslagstjenesteConfiguration.Avsendersertifikat)
        {
            Environment = oppslagstjenesteConfiguration.Miljø;
        }

        public Miljø Environment { get; set; }

        public void Validate()
        {
            var signedXmlWithAgnosticId = new SignedXmlWithAgnosticId(ResponseContainer.Envelope);
            signedXmlWithAgnosticId.LoadXml(ResponseContainer.HeaderSignatureElement);

            // Ensures received envelope contains signature confirmation, and that body and ids matches header signature
            ValidateSignatureReferences(ResponseContainer.HeaderSignatureElement, signedXmlWithAgnosticId,
                new[] {"/env:Envelope/env:Header/wsse:Security/wsse11:SignatureConfirmation", "/env:Envelope/env:Body"});

            // Validating SignatureConfirmation
            PerformSignatureConfirmation(ResponseContainer.HeaderSecurityElement);

            CheckTimestamp(TimeSpan.FromSeconds(2000));

            ValidateResponseCertificate(signedXmlWithAgnosticId);
        }

        internal void ValidateResponseCertificate(SignedXmlWithAgnosticId signed)
        {
            var signature = ResponseContainer.HeaderBinarySecurityToken.InnerText;
            var value = Convert.FromBase64String(signature);
            var certificate = new X509Certificate2(value);

            var isValidCertificateChain = Environment.CertificateChainValidator.ErGyldigSertifikatkjede(certificate);
            var isValidCertificate = CertificateValidator.IsValidCertificate(certificate, "991825827");

            var isAcceptedCertificate = isValidCertificateChain && isValidCertificate;
            if (!isAcceptedCertificate)
            {
                throw new SecurityException("Sertifikatet i responsen er ikke gyldig.");
            }

            var key = certificate.PublicKey.Key;
            if (!signed.CheckSignature(key))
                throw new SecurityException("Signaturen i motatt svar er ikke gyldig");
        }
    }
}