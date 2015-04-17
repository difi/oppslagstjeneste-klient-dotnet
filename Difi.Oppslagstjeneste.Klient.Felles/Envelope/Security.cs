using System;
using System.Xml;

namespace Difi.Oppslagstjeneste.Klient.Felles.Envelope
{
    

    public class Security : EnvelopeXmlPart
    {
        private TimeSpan? _timespan;

        public Security(EnvelopeSettings envelopeSettings, XmlDocument xmlDocument, TimeSpan? timestampexpirey)
            : base(envelopeSettings, xmlDocument)
        {
            _timespan = timestampexpirey;
        }

        public override XmlNode Xml()
        {
            var securityElement = Context.CreateElement("wsse", "Security", Navnerom.wsse);
            securityElement.SetAttribute("xmlns:wsu", Navnerom.wsu);

            // BinarySecurityTokenElement

            //            securityElement.AppendChild(BinarySecurityTokenElement());

            // TimestampElement
            if (_timespan.HasValue)
                securityElement.AppendChild(TimestampElement());
            return securityElement;

        }
        /*
        private XmlElement BinarySecurityTokenElement()
        {
            XmlElement binarySecurityToken = Context.CreateElement("wsse", "BinarySecurityToken", Navnerom.wsse);
            binarySecurityToken.SetAttribute("Id", Navnerom.wsu, Settings.GuidHandler.BinarySecurityTokenId);
            binarySecurityToken.SetAttribute("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            binarySecurityToken.SetAttribute("ValueType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-x509-token-profile-1.0#X509v3");
            binarySecurityToken.InnerText = Convert.ToBase64String(Settings.Databehandler.Sertifikat.RawData);
            return binarySecurityToken;
        }*/

        private XmlElement TimestampElement()
        {
            XmlElement timestamp = Context.CreateElement("wsu", "Timestamp", Navnerom.wsu);
            {
                var utcNow = DateTime.UtcNow.AddMinutes(-1);
                timestamp.AppendChildElement("Created", "wsu", Navnerom.wsu, utcNow.ToString(DateUtility.DateFormat));
                timestamp.AppendChildElement("Expires", "wsu", Navnerom.wsu, utcNow.Add(_timespan.Value).ToString(DateUtility.DateFormat));
            }

            timestamp.SetAttribute("Id", Navnerom.wsu, Settings.TimestampId);
            return timestamp;
        }
    }
}
