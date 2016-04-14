using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    internal class OppslagstjenesteValidatorWithStubbedCheckTimestamp : OppslagstjenesteValidator
    {
        public OppslagstjenesteValidatorWithStubbedCheckTimestamp(XmlDocument sentDocument, ResponseContainer responseContainer,
            OppslagstjenesteKonfigurasjon oppslagstjenesteConfiguration)
            : base(sentDocument, responseContainer, oppslagstjenesteConfiguration)
        {
        }

        protected override void CheckTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }
    }
}