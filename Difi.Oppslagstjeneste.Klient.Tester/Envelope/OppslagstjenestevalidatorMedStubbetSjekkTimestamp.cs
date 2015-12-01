using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Envelope;

namespace Difi.Oppslagstjeneste.Klient.Tester.Envelope
{
    public class OppslagstjenestevalidatorMedStubbetSjekkTimestamp : Oppslagstjenestevalidator
    {
        public OppslagstjenestevalidatorMedStubbetSjekkTimestamp(XmlDocument sendtDokument, XmlDocument mottattDokument, OppslagstjenesteInstillinger oppslagstjenesteInstillinger, Miljø miljø) 
            : base(sendtDokument, mottattDokument, oppslagstjenesteInstillinger, miljø)
        {
        }

        protected override void SjekkTimestamp(TimeSpan timeSpan)
        {
            //Do nothing.
        }


    }
}
