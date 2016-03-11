using System;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class EnvelopeSettings
    {
        public EnvelopeSettings()
        {
            BodyId = string.Format("id-{0}", Guid.NewGuid());
            TimestampId = string.Format("TS-{0}", Guid.NewGuid());
        }

        public string BodyId { get; set; }

        public virtual string TimestampId { get; set; }

        public string BinarySecurityId { get; set; }
    }
}