---
identifier: installeresertifikater
title: Installere sertifikater
layout: default
---

For å sende forespørsler til Oppslagstjenesten trenger du å installere sertifikater på maskinen. Grunnen til at de skal installeres er i hovedsak sikkerhet. Når du installerer ditt private avsendersertifikat på datamaskinen så blir du spurt om passord. Dette skjer kun én gang, og etter dette kan du bruke sertifikatet i koden uten å eksponere passordet. 

Installere virksomhetssertifikat

> Virksomhetssertifikatet brukes av virksomheten for å signere forespørsler som går til Oppslagstjenesten.

1.  Dobbeltklikk på sertifikatet (Sertifikatnavn.p12)
1.  Velg at sertifikatet skal lagres i _Current User_ og trykk _Next_
1.  Filnavn skal nå være utfylt. Trykk _Next_
1.  Skriv inn passord for privatekey og velg _Mark this key as exportable ..._, trykk _Next_
1.  Velg _Automatically select the certificate store based on the type of certificate_
1.  _Next_ og _Finish_
1.  Får du spørsmål om å godta sertifikatet så gjør det.
1.  Du skal da få en dialog som sier at importeringen var vellykket. Trykk _Ok_.

### Finne installert sertifikat</h3>

`OppslagstjenesteKlient` tar inn `OppslagstjenesteKonfigurasjon`, som igjen tar inn `thumbprint` direkte:

{% highlight csharp %}
var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø, avsendersertifikatThumbprint);
var klient = OppslagstjenesteKlient(konfigurasjon);
{% endhighlight %}

For å finne _thumbprint_ så er det lettest å gjøre det vha _Microsoft Management Console_ (mmc.exe).

1. Velg _File_ -> _Add/Remove Snap-in..._ 
1. Merk _Certificates_ og trykk _Add >_
1. Hvis sertifikatet ble installert i _Current User_ velges _My user account_, og hvis det er installert på _Local Machine_ så velges _Computer Account_. Klikk _Finish_ og så _OK_
1. Ekspander _Certificates_-noden, velg _Personal_ og åpne _Certificates_
1. Dobbeltklikk på sertifikatet du installerte
1. Velg _Details_, scroll ned til _Thumbprint_ og kopier

Ønsker du å sende inn sertifikater du har allerede har initialisert, kan du bruke konstruktøren `OppslagstjenesteKonfigurasjon(Miljø,X509Certificate2)`.