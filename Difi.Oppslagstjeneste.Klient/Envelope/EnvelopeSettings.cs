using System;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal sealed class EnvelopeSettings
    {
        public EnvelopeSettings()
        {
            BodyId = $"id-{Guid.NewGuid()}";
            TimestampId = $"TS-{Guid.NewGuid()}";
            BinarySecurityId = $"X509-{Guid.NewGuid()}";
        }

        public string BodyId { get; set; }

        public string TimestampId { get; set; }

        public string BinarySecurityId { get; set; }

        
    }
}