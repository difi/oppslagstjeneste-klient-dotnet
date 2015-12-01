using System;
using System.Diagnostics;
using Difi.Felles.Utility;
using Difi.SikkerDigitalPost.Klient;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon : Klientkonfigurasjon
    {
        public OppslagstjenesteKonfigurasjon(Miljø miljø)
            :base(miljø)
        {
        }
    }
}
