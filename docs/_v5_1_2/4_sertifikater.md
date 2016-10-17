---
title: Installere sertifikater
identifier: installeresertifikater
layout: default
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

<h3 id="finneinstallertsertifikat">Finne installert sertifikat</h3>

<code>OppslagstjenesteKlient</code> tar inn <code>OppslagstjenesteKonfigurasjon</code>, som igjen tar inn _thumbprint_ direkte:

{% highlight csharp %}
var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsendersertifikatThumbprint);
var klient = OppslagstjenesteKlient(konfigurasjon);
{% endhighlight %}

For å finne _thumbprint_ så er det lettest å gjøre det vha _Microsoft Management Console_ (mmc.exe). 

1.  Start mmc.exe (Trykk windowstast og skriv _mmc.exe_)
2.  Velg _File_ -> _Add/Remove Snap-in..._ 
3.  Merk _Certificates_ og trykk _Add >_
4.  Velg _My user account_ og trykk _Finish_
5.	Åpne mappe for sertifikat og finn avsendersertifikat: Åpne noden _Certificates - Current User - Personal - Certificates_
6. 	Dobbeltklikk på sertifikatet du installerte
7.	Velg _Details_, scroll ned til _Thumbprint_ og kopier

Ønsker du å sende inn sertifikater du har allerede har initialisert, kan du kalle konstruktøren som tar inn <code> X509Certificate2</code>.