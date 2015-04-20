using System;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using ApiClientShared;

namespace Difi.Oppslagstjeneste.Klient.Testklient
{
    class Program
    {
        static void Main(string[] args)
        {
            var storeMy = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            storeMy.Open(OpenFlags.ReadOnly);

            var avsenderSertifikat = storeMy.Certificates.Find(X509FindType.FindByThumbprint, "B0CB922214D11E8CE993838DB4C6D04C0C0970B8", true)[0];
            
            ResourceUtility rr = new ResourceUtility("Difi.Oppslagstjeneste.Klient.Testklient.Resources");
            var certBytes = rr.ReadAllBytes(true, "cert.idporten-ver2.difi.no-v2.crt");
            var valideringsSertifikat = new X509Certificate2(certBytes);

            var konfig = new OppslagstjenesteKonfigurasjon
            {
                ServiceUri = new Uri("https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4")
            };

            Oppslagstjeneste register = new Oppslagstjeneste(avsenderSertifikat, valideringsSertifikat, konfig);

            var endringer = register.HentEndringer(886730, Informasjonsbehov.Kontaktinfo | Informasjonsbehov.Sertifikat | Informasjonsbehov.SikkerDigitalPost | Informasjonsbehov.Person);
            var personer = register.HentPersoner(new string[] { "080770002Leg92" }, Informasjonsbehov.Sertifikat | Informasjonsbehov.Kontaktinfo | Informasjonsbehov.SikkerDigitalPost);
            var printSertifikat = register.HentPrintSertifikat();
        }
    }
}
