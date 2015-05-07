using System;
using System.Diagnostics;

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
            
            var avsenderSertifikat = CertificateUtility.SenderCertificate("B0CB922214D11E8CE993838DB4C6D04C0C0970B8", Language.Norwegian);
            var valideringssertifikat = CertificateUtility.ReceiverCertificate("a4 7d 57 ea f6 9b 62 77 10 fa 0d 06 ec 77 50 0b af 71 c4 32", Language.Norwegian);
            
            OppslagstjenesteKlient register = new OppslagstjenesteKlient(avsenderSertifikat, valideringssertifikat, konfig);

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
