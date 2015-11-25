﻿using System;
using Difi.Felles.Utility;

namespace Difi.Oppslagstjeneste.Klient
{
    public class Miljø : AbstraktMiljø
    {
        internal Miljø(Uri url, Sertifikatkjedevalidator sertifikatkjedevalidator)
        {
            Sertifikatkjedevalidator = sertifikatkjedevalidator;
            Url = url;
        }

        public static Miljø FunksjoneltTestmiljø
        {
            get
            {
                return new Miljø(
                    new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4"),
                    new Sertifikatkjedevalidator(SertifikatkjedeUtility.FunksjoneltTestmiljøSertifikater())
                    );

            }
        }

        public static Miljø Produksjonsmiljø
        {
            get
            {
                return new Miljø(
                    new Uri("https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v4"),
                    new Sertifikatkjedevalidator(SertifikatkjedeUtility.ProduksjonsSertifikater())
                    );
            }
        }
    }
}