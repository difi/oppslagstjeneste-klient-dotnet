# Oppslagstjeneste-klient-dotnet

[![Build status](https://ci.appveyor.com/api/projects/status/lddp0g47vbmig3n5/branch/master?svg=true)](https://ci.appveyor.com/project/difi/oppslagstjeneste-klient-dotnet/branch/master)

[Online dokumentasjon finner du her!](http://difi.github.io/oppslagstjeneste-klient-dotnet)

## arbeidsflyt ved lokal utvikling:

- Bygg lokalt: `dotnet build`.
- Kjør tester lokalt: `SMOKE_TEST_CERTIFICATE_PATH="" SMOKE_TEST_CERTIFICATE_PASSWORD="" dotnet test`
- Kjør tester i container: `docker-compose up --build`
- Push branch til github og lag PR.

Sett `SignAssembly` til false i prosjektfilene for å deaktivere strong-named assemblies under utvikling.
Har du tilgang til signingkey (digipost-utviklere) kan du evt dekryptere `signingkey.snk.enc` først.
Man kan verifisere at DLL-en er strong-named ved å benytte `sn -v <path-to-dll>`.


NB: på grunnn av begrensninger knyttet til kjøring på linux (Unix LocalMachine X509Store is limited to the Root and
CertificateAuthority stores.) ifbm sertifikater er 7 tester utelatt ved docker-compose og github action kjøring. Det
erikke prioritert å få disse testene til å kjøre på github actions da dette biblioteket snart skal erstattes. Sjekk
derfor at alle testene i det minste kjører grønt på din maskin...

**Notat om sertifikat i smoketest:** \  
Sertifikatet som er i bruk i smoketestene er et testsertifikat med tilhørende privat nøkkel, signert av Buypass for
Digipost. Public del med kjeden ligger plassert
i `Difi.Oppslagstjeneste.Klient.Resources/Certificate/Data/TestChain/smoketest.cer`. Ved lokal utvikling på dette repoet
må man ha tilgang til dette sertifikatet (`SMOKE_TEST_CERTIFICATE_PATH` og `SMOKE_TEST_CERTIFICATE_PASSWORD`). Se intern
dokumentasjon (digipost) for dette (sertifikat med navn Bring_Digital_Signature_Key_Encipherment_Data_Encipherment).
