using System;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Svar;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    public class OppslagstjenestevalidatorMedStubbetSjekkTimestamp : Oppslagstjenestevalidator
    {
        public OppslagstjenestevalidatorMedStubbetSjekkTimestamp(XmlDocument sendtDokument, ResponseDokument responseDokument,
            OppslagstjenesteInstillinger oppslagstjenesteInstillinger, Miljø miljø)
            : base(sendtDokument, responseDokument, oppslagstjenesteInstillinger, miljø)
        {
        }

        protected override void SjekkTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }
    }
}