using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Difi.Oppslagstjeneste.Klient.Testklient
{
    class Program
    {
        static void Main(string[] args)
        {
            var konfig = new OppslagstjenesteKonfigurasjon
            {
                ServiceUri = new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4") //test
                //ServiceUri = new Uri("https://kontaktinfo-ws.difi.no/kontaktinfo-external/ws-v4") //prod


            };
            
            Logging.Initialize(konfig);
            Logging.Log(TraceEventType.Information, "> Starter program!");

            
            var avsendersertifikatThumbprint = CertificateIssuedToPostenNorgeAsIssuedByBuypassClass3Test4Ca3();

            var mottakersertifikatValideringThumbprint = CertificateIssuedToDirektoratetForForvaltningOgIktIssuedByBuypassClass3Ca3();
            

            OppslagstjenesteKlient register = new OppslagstjenesteKlient(avsendersertifikatThumbprint, mottakersertifikatValideringThumbprint, konfig);

            var endringer = register.HentEndringer(983831, 
                Informasjonsbehov.Kontaktinfo | 
                Informasjonsbehov.Sertifikat | 
                Informasjonsbehov.SikkerDigitalPost |
                Informasjonsbehov.Person);
            
            var personer = register.HentPersoner(new string[] { "08077000292" }, 
                Informasjonsbehov.Sertifikat | 
                Informasjonsbehov.Kontaktinfo | 
                Informasjonsbehov.SikkerDigitalPost);

            var cert = ExportToPEM(personer.ElementAt(0).X509Sertifikat);
           
            var printSertifikat = register.HentPrintSertifikat();
            Console.ReadKey();
        }

        private static string CertificateIssuedToPostenNorgeAsIssuedByBuypassClass3Test4Ca3()
        {
            return "8702F5E55217EC88CF2CCBADAC290BB4312594AC";
        }

        private static string CertificateIssuedToDirektoratetForForvaltningOgIktIssuedByBuypassClass3Ca3()
        {
            return "a47d57eaf69b627710fa0d06ec77500baf71c432";
        }

        public static string ExportToPEM(X509Certificate cert)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");

            return builder.ToString();
        }
    }
}
