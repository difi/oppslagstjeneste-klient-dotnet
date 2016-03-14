using System.Diagnostics;
using Difi.Felles.Utility;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon : GeneriskKlientkonfigurasjon
    {
        public OppslagstjenesteKonfigurasjon(Miljø miljø, string sendPåVegneAv = null)
            : base(miljø)
        {
            Felles.Utility.Logger.TraceSource = new TraceSource("Difi.Oppslagstjeneste.Klient");
            Logger = Felles.Utility.Logger.TraceLogger();
            SendPåVegneAv = sendPåVegneAv;
        }

        public string SendPåVegneAv { get; set; }
    }
}