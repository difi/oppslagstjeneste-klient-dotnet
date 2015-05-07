---
title: Installere sertifikater
id: installeresertifikater
layout: default
description: Installere sertifikater for signering og kryptering av post
isHome: false
---

For å sende forespørsler til Oppslagstjenesten trenger du å installere sertifikater på maskinen. Grunnen til at de skal installeres
er i hovedsak sikkerhet. Når du installerer ditt private avsendersertifikat på datamaskinen så blir du spurt om passord. Dette skjer 
kun én gang, og etter dette kan du bruke sertifikatet i koden uten å eksponere passordet. 

<h3 id="databehandlersertifikat">Installere avsendersertifikat/virksomhetssertifikat</h3>

<blockquote> Avsendersertifikatet brukes av Virksomhet for å signere forespørsler som går til Oppslagstjenesten.  </blockquote>

1.  Dobbeltklikk på sertifikatet (Sertifikatnavn.p12)
2.  Velg at sertifikatet skal lagres i _Current User_ og trykk _Next_
3.  Filnavn skal nå være utfylt. Trykk _Next_
4.  Skriv inn passord for privatekey og velg _Mark this key as exportable ..._, trykk _Next_
5.  Velg _Automatically select the certificate store based on the type of certificate_
6.  _Next_ og _Finish_
7.  Får du spørsmål om å godta sertifikatet så gjør det.
8.  Du skal da få en dialog som sier at importeringen var vellykket. Trykk _Ok_.

<h3 id="mottakersertifikat">Legg inn valideringssertifikat i certificate store</h3>

<blockquote> Valideringssertifikat vil være sertifikatet som brukes for å validere svar fra Oppslagstjenesten.</blockquote>

Hvis du skal kjøre mot Difis testserver(https://kontaktinfo-ws-ver2.difi.no/kontaktinfo-external/ws-v4), så skal du bruke [Test_Difi.cer](https://github.com/difi/oppslagstjeneste-klient-dotnet/tree/gh-pages/cert/Test_Difi.cer). Høyreklikk og lagre til disk.  

1.  Start mmc.exe (Trykk windowstast og skriv _mmc.exe_)
2.  Velg _File_ -> _Add/Remove Snap-in..._ 
3.  Merk _Certificates_ og trykk _Add >_
4.  Velg _My user account_ og trykk _Finish_
5.  Åpne noden _Certificates - Current User_ - Trusted People - Certificates_
6.  Høyreklikk på _Trusted People_ og velg _All Tasks_ -> _Import..._
7.  Trykk _Next_
8.  Finn mottaker-sertifikatet (Sertifikatnavn.cer) og legg det til. Trykk _Next_
9.  Sertifikatet skal legges til i _Trusted People_
10. _Next_ og _Finish_
11. Du skal da få en dialog som sier at importeringen var vellykket. Trykk _Ok_.

<h3 id="mottakersertifikat">Finne installert sertifikat</h3>

<code>OppslagstjenesteKlient</code> har støtte for å ta inn _thumbprint_ direkte:

{% highlight csharp %}
OppslagstjenesteKlient(avsendersertifikatThumbprint, valideringssertifikatThumbprint)
{% endhighlight %}

 . For å finne _thumbprint_ så er det lettest å gjøre det vha _Microsoft Management Console_ (mmc.exe). 

1.  Start mmc.exe (Trykk windowstast og skriv _mmc.exe_)
2.  Velg _File_ -> _Add/Remove Snap-in..._ 
3.  Merk _Certificates_ og trykk _Add >_
4.  Velg _My user account_ og trykk _Finish_
5.	Åpne mappe for sertifikat
	1. Avsendersertifikat: Åpne noden _Certificates - Current User - Personal - Certificates_
	2. Valideringssertifikat: Åpne noden _Certificates - Current User - Trusted People - Certificates_
6. 	Dobbeltklikk på sertifikatet du installerte
7.	Velg _Details_, scroll ned til _Thumbprint_ og kopier
8.	VIKTIG: Hvis du får problemer i kode med at sertifikat ikke finnes, så kan det hende du får med en usynling _BOM_(Byte Order Mark). Slett derfor denne med å sette peker før første tegn i thumbprint i en teksteditor. Hvis det var en BOM der så forsvant ikke det første synlige tegnet i thumbprint. 

Ønsker du å sende inn sertifikater du har allerede har initialisert, kan du kalle konstruktøren som tar inn <code> X509Certificate2</code>.