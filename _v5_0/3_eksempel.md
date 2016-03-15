---
title: Eksempel på kall
id: eksempelforsending
layout: default
isHome: true
---

Det er bare tre kall du kan gjøre mot oppslagstjenesten; hente endringer, hente personer og hente printsertifikat. Her er et minimumseksempel på hva du må ha når du har fått hentet sertifikatene som er installert.

{% highlight csharp%}
var register = new OppslagstjenesteKlient(avsendersertifikatThumbprint, konfigurasjon);

var endringer = register.HentEndringer(600,
    Informasjonsbehov.Person ,
    Informasjonsbehov.Kontaktinfo ,
    Informasjonsbehov.Sertifikat ,
    Informasjonsbehov.SikkerDigitalPost ,
    Informasjonsbehov.VarslingsStatus

var personer = register.HentPersoner(new[] {"08077000292"},
    Informasjonsbehov.Kontaktinfo ,
    Informasjonsbehov.Sertifikat ,
    Informasjonsbehov.SikkerDigitalPost ,
    Informasjonsbehov.VarslingsStatus

var printSertifikat = register.HentPrintSertifikat();

{% endhighlight %}