using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ApiClientShared;
using ApiClientShared.Enums;

namespace Difi.Oppslagstjeneste.Klient.Testklient
{
    class Program
    {
        static void Main(string[] args)
        {
            var konfig = new OppslagstjenesteKonfigurasjon
            {
                ServiceUri = new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4")
            };
            
            Logging.Initialize(konfig);
            Logging.Log(TraceEventType.Information, "> Starter program!");

            OppslagstjenesteKlient register = new OppslagstjenesteKlient("8702F5E55217EC88CF2CCBADAC290BB4312594AC", "a4 7d 57 ea f6 9b 62 77 10 fa 0d 06 ec 77 50 0b af 71 c4 32", konfig);

            var endringer = register.HentEndringer(983831, 
                Informasjonsbehov.Kontaktinfo | 
                Informasjonsbehov.Sertifikat | 
                Informasjonsbehov.SikkerDigitalPost |
                Informasjonsbehov.Person);
            
            var personer = register.HentPersoner(new string[] { "08077000292" }, 
                Informasjonsbehov.Sertifikat | 
                Informasjonsbehov.Kontaktinfo | 
                Informasjonsbehov.SikkerDigitalPost);
           
            var printSertifikat = register.HentPrintSertifikat();
            Console.ReadKey();
        }
    }
}
