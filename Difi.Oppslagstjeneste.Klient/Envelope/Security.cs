using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene;

namespace Difi.Oppslagstjeneste.Klient.Envelope
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
            var securityElement = Context.CreateElement("wsse", "Security", Navnerom.WssecuritySecext10);
            securityElement.SetAttribute("xmlns:wsu", Navnerom.WssecurityUtility10);


            if (_timespan.HasValue)
                securityElement.AppendChild(TimestampElement());
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