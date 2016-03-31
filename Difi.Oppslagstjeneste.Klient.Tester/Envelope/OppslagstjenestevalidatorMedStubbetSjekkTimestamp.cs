using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    internal class OppslagstjenestevalidatorMedStubbetSjekkTimestamp : Oppslagstjenestevalidator
    {
        public OppslagstjenestevalidatorMedStubbetSjekkTimestamp(XmlDocument sendtDokument, ResponseContainer responseContainer,
            OppslagstjenesteKonfigurasjon oppslagstjenesteKonfigurasjon)
            : base(sendtDokument, responseContainer, oppslagstjenesteKonfigurasjon)
        {
        }

        protected override void SjekkTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }
    }
}