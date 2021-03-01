using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Difi.Oppslagstjeneste.Klient.Domene.Entiteter.Enums;
using log4net;

namespace Difi.Oppslagstjeneste.Klient.Testklient
{
    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            var avsendersertifikatThumbprint = CertificateIssuedToPostenNorgeAsIssuedByBuypassClass3Test4Ca3();
            var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, avsendersertifikatThumbprint) {LoggForespørselOgRespons = true};
            Log.Debug("> Starter program!");

            //konfigurasjon.SendPåVegneAv = "984661185";

            var register = new OppslagstjenesteKlient(konfigurasjon);

            //var endringer = register.HentEndringer(600,
            //    Informasjonsbehov.Person,
            //    Informasjonsbehov.Kontaktinfo,
            //    Informasjonsbehov.Sertifikat,
            //    Informasjonsbehov.SikkerDigitalPost,
            //    Informasjonsbehov.VarslingsStatus

            var personer = register.HentPersoner(new[] {"08077000292"},
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
                Informasjonsbehov.VarslingsStatus
                );

            var cert = ExportToPEM(personer.ElementAt(0).X509Sertifikat);

            var printSertifikat = register.HentPrintSertifikat();
            Console.WriteLine("Ferdig med oppslag ...");

            Console.ReadKey();
        }

        private static string CertificateIssuedToPostenNorgeAsIssuedByBuypassClass3Test4Ca3()
        {
            // POSTEN NORGE AS TEST (Trusted in keychain)
            return "875C927720AE3228BE73460940EB71E90D039F3B";
        }

        private static string CertificateIssuedToBringAsIssuedByBuypassClass3Test4Ca3()
        {
            return "2d 7f 30 dd 05 d3 b7 fc 7a e5 97 3a 73 f8 49 08 3b 20 40 ed";
        }


        public static string ExportToPEM(X509Certificate cert)
        {
            var builder = new StringBuilder();

            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");

            return builder.ToString();
        }
    }
}
