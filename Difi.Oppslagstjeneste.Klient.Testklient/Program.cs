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
            var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon2, avsendersertifikatThumbprint);

            Log.Debug("> Starter program!");

            //konfigurasjon.SendPåVegneAv = "984661185";

            var register = new OppslagstjenesteKlient(konfigurasjon);

            var endringer = register.HentEndringer(600,
                Informasjonsbehov.Person,
                Informasjonsbehov.Kontaktinfo,
                Informasjonsbehov.Sertifikat,
                Informasjonsbehov.SikkerDigitalPost,
                Informasjonsbehov.VarslingsStatus
                );

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
            return "8702F5E55217EC88CF2CCBADAC290BB4312594AC";
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