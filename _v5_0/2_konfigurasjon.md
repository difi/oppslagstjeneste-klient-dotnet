---
title: Konfigurasjon
id: konfigurasjon
layout: default
isHome: true
---

OppslagstjenesteKonfigurasjon initieres med hvilket miljø man ønsker å kjøre mot. FunksjoneltTestmiljø eller Produksjonsmiljø.

{% highlight csharp%}

var testmiljøKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø);
var produksjonsmiljøKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.Produksjonsmiljø);

{% endhighlight%}
<h3 id="proxy">Proxy</h3>
For å bruke proxy setter man `ProxyHost`,`ProxyPort` og `ProxyScheme` i konfigurasjonen:
{% highlight csharp%}
var oppslagstjenesteKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø)
{
    ProxyHost = "proxyhost",
    ProxyPort = 3333,
    ProxyScheme = "https"
};
{% endhighlight%}
<h3 id="sendpaavegneav">På vegne av</h3>

For å gjøre oppslag på vegne av en annen virksomhet settes organisasjonsnummeret til gitt bedrift i `SendPåVegneAv` propertien:
{% highlight csharp%}

var testmiljøKonfigurasjon = new OppslagstjenesteKonfigurasjon(Miljø.FunksjoneltTestmiljø);
testmiljøKonfigurasjon.SendPåVegneAv = "984661185";

{% endhighlight%}