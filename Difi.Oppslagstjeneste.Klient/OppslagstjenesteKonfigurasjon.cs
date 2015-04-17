using System;
using Difi.Felles;

namespace Difi.Oppslagstjeneste.Klient
{
    public class OppslagstjenesteKonfigurasjon : Klientkonfigurasjon
    {
        protected override string Prefix
        {
            get { return "Oppslagstjeneste"; }
        }
                
        public OppslagstjenesteKonfigurasjon() : base()
        {
            ServiceUri = base.SetFromAppConfig<Uri>(Prefix + ":ServiceUri", new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4"));
        }
    }
}
