---
title: Eksempel på kall
id: eksempelforsending
layout: default
isHome: true
---

Det er bare tre kall du kan gjøre mot oppslagstjenesten; hente endringer, hente personer og hente printsertifikat. Her er et minimumseksempel på hva du må ha når du har fått hentet sertifikatene som er installert.

{% highlight csharp%}
OppslagstjenesteKlient klient = new OppslagstjenesteKlient(avsenderSertifikat, valideringsSertifikat, konfig);

var endringer = klient.HentEndringer(886730, 
    Informasjonsbehov.Kontaktinfo | 
    Informasjonsbehov.Sertifikat | 
    Informasjonsbehov.SikkerDigitalPost |
    Informasjonsbehov.Person);

var personer = klient.HentPersoner(new string[] { "08077000292" }, 
    Informasjonsbehov.Sertifikat | 
    Informasjonsbehov.Kontaktinfo | 
    Informasjonsbehov.SikkerDigitalPost);

var printSertifikat = klient.HentPrintSertifikat();
{% endhighlight %}