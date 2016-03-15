---
title: Eksempel på kall
id: eksempelforsending
layout: default
isHome: true
---

Det er bare tre kall du kan gjøre mot oppslagstjenesten; hente endringer, hente personer og hente printsertifikat. Her er et minimumseksempel på hva du må ha når du har fått hentet sertifikatene som er installert.

{% highlight csharp%}
var register = new OppslagstjenesteKlient(avsendersertifikatThumbprint, konfigurasjon);

const int fraEndringsNummer = 600;
var endringer = register.HentEndringer(fraEndringsNummer,
	Informasjonsbehov.Person ,
	Informasjonsbehov.Kontaktinfo ,
	Informasjonsbehov.Sertifikat ,
	Informasjonsbehov.SikkerDigitalPost ,
	Informasjonsbehov.VarslingsStatus
	);

var personidentifikator = new[] {"08077000292"};
var personer = register.HentPersoner(personidentifikator,
    Informasjonsbehov.Kontaktinfo ,
    Informasjonsbehov.Sertifikat ,
    Informasjonsbehov.SikkerDigitalPost ,
    Informasjonsbehov.VarslingsStatus
    );


var printSertifikat = register.HentPrintSertifikat();

{% endhighlight %}