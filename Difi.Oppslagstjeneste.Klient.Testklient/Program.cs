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
            var oppslagstjenesteKonfigurasjon = new Konfigurasjon(Miljø.FunksjoneltTestmiljø);

            AbstraktLogger.Initialize(oppslagstjenesteKonfigurasjon);
            AbstraktLogger.Log(TraceEventType.Information, "> Starter program!");
            
            var avsendersertifikatThumbprint = CertificateIssuedToPostenNorgeAsIssuedByBuypassClass3Test4Ca3();
            var mottakersertifikatValideringThumbprint = CertificateIssuedToDirektoratetForForvaltningOgIktIssuedByBuypassClass3Ca3();

            OppslagstjenesteKlient register = new OppslagstjenesteKlient(avsendersertifikatThumbprint, mottakersertifikatValideringThumbprint, oppslagstjenesteKonfigurasjon);

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
