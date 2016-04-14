using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;
using Difi.Oppslagstjeneste.Klient.Extensions;
using Difi.Oppslagstjeneste.Klient.Security;

namespace Difi.Oppslagstjeneste.Klient.Envelope
{
    internal class Security : EnvelopeXmlPart
    {
        private TimeSpan? _timespan;

        public Security(X509Certificate2 senderCertificate, EnvelopeSettings envelopeSettings, XmlDocument xmlDocument, TimeSpan? timestampexpirey)
            : base(envelopeSettings, xmlDocument)
        {
            _timespan = timestampexpirey;
            SenderCertificate = senderCertificate;
        }

        public X509Certificate2 SenderCertificate { get; set; }

        public override XmlNode Xml()
        {
            var securityElement = Context.CreateElement("wsse", "Security", Navnerom.WssecuritySecext10);
            securityElement.SetAttribute("xmlns:wsu", Navnerom.WssecurityUtility10);

            if (_timespan.HasValue)
                securityElement.AppendChild(TimestampElement());

            var securityToken = Context.ImportNode(new SecurityTokenReferenceClause(SenderCertificate, Settings.BinarySecurityId).GetTokenXml(), true);
            securityElement.AppendChild(securityToken);

            return securityElement;
        }

        private XmlElement TimestampElement()
        {
            var timestamp = Context.CreateElement("wsu", "Timestamp", Navnerom.WssecurityUtility10);
            {
                var utcNow = DateTime.UtcNow;
                timestamp.AppendChildElement("Created", "wsu", Navnerom.WssecurityUtility10,
                    utcNow.ToString(DateUtility.DateFormat));
                timestamp.AppendChildElement("Expires", "wsu", Navnerom.WssecurityUtility10,
                    utcNow.Add(_timespan.Value).ToString(DateUtility.DateFormat));
            }

            timestamp.SetAttribute("Id", Navnerom.WssecurityUtility10, Settings.TimestampId);
            return timestamp;
        }
    }
}