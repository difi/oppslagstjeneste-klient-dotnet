using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    internal class OppslagstjenestevalidatorMedStubbetSjekkTimestamp : Oppslagstjenestevalidator
    {
        public OppslagstjenestevalidatorMedStubbetSjekkTimestamp(XmlDocument sendtDokument, ResponseContainer responseContainer,
            OppslagstjenesteInstillinger oppslagstjenesteInstillinger, Miljø miljø)
            : base(sendtDokument, responseContainer, oppslagstjenesteInstillinger, miljø)
        {
        }

        protected override void SjekkTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }
    }
}