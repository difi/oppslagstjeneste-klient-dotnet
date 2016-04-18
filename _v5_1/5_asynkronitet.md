---
title: Async/Sync
id: async_sync
layout: default
isHome: false
---

Alle operasjonene/metodene i Oppslagstjenesten har både synkrone og asynkrone metoder. Velg det som passer din applikasjon. Klienten initialiserer på samme måte og det er kun gitt metode som styrer hvorvidt den er asynkron eller ikke.

<h3 id="Asynkrone metoder">Asynkrone metoder</h3>

Metodene som er asynkrone returnerer en <code>Task<></code> av objektet den synkrone metoden returnerer. Man kan <code>await`e</code> svaret direkte eller ventet på at <code>Task-en</code> skal bli ferdig på annet vis. 

{% highlight csharp%}
var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, avsendersertifikatThumbprint);
var register = new OppslagstjenesteKlient(konfigurasjon);

var endringer = await register.HentEndringerAsynkront(fraEndringsNummer,
	Informasjonsbehov.Person ,
	Informasjonsbehov.Kontaktinfo ,
	Informasjonsbehov.Sertifikat ,
	Informasjonsbehov.SikkerDigitalPost ,
	Informasjonsbehov.VarslingsStatus
	);

//Returnerer Task<> 
var personidentifikator = new[] {"08077000292"};
Task<IEnumerable<Person>> personer = register.HentPersonerAsynkront(personidentifikator,
    Informasjonsbehov.Kontaktinfo ,
    Informasjonsbehov.Sertifikat ,
    Informasjonsbehov.SikkerDigitalPost ,
    Informasjonsbehov.VarslingsStatus
    );
//Vent på at tasken blir ferdig..

var printSertifikat = await register.HentPrintSertifikatAsynkront();

{% endhighlight %}

<h3 id="Synkrone metoder">Synkrone metoder</h3>
{% highlight csharp%}
var konfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljøVerifikasjon1, avsendersertifikatThumbprint);
var register = new OppslagstjenesteKlient(konfigurasjon);

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