using System;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    public class EnvelopeSettings
    {        
        public SoapVersion SoapVersion { get; set; }
     
        public string BodyId { get; set; }
        public virtual string TimestampId { get; set; }

        public string SoapNamespace
        {
            get
            {
                if (SoapVersion == SoapVersion.Soap11)
                    return Navnerom.SoapEnvelope;
                else
                    return Navnerom.SoapEnvelopeEnv12;
            }
        }
        
        public EnvelopeSettings(SoapVersion version)
        {
            SoapVersion = version;

            BodyId = string.Format("id-{0}", Guid.NewGuid());
            TimestampId = String.Format("TS-{0}", Guid.NewGuid());
        }
    }
}
