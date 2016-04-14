using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Tests.Envelope
{
    internal class OppslagstjenestevalidatorWithStubbedCheckTimestamp : Oppslagstjenestevalidator
    {
        public OppslagstjenestevalidatorWithStubbedCheckTimestamp(XmlDocument sendtDokument, ResponseContainer responseContainer,
            OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
            : base(sendtDokument, responseContainer, oppslagstjenesteKonfigurasjon)
        {
        }

        protected override void CheckTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }
    }
}