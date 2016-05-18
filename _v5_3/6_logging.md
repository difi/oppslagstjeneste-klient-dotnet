---
id: Logging
title: Logging
layout: default
isHome: false
---

<h3 id="genereltOmlogging">Generelt</h3>
Klienten bruker _Common.Logging API_ for å abstrahere logging. Det er opp til brukeren å imlementere API med et passende loggrammeverk, men vi viser hvordan dette kan gjøres med Log4Net.

Loggnivå `DEBUG` vil logge resultat for forespørsler som går bra og de som feiler, `WARN` bare for feilede forespørsler eller verre, mens `ERROR` vil aldri forekomme. Disse loggerne vil være under `Difi.Oppslagstjeneste.Klient`

<h3 id="log4net">Implementere Log4Net som logger</h3>

1. Installer Nuget-pakke `Common.Logging.Log4Net`. Denne vil da også installere avhengighetene `Common.Logging.Core` og `Common.Logging`. Merk at versjoneringen her er litt underlig, men et søk i Nuget Gallery vil f.eks. vise at Log4Net 2.0.3 har pakkenavn _Log4net [1.2.13] 2.0.3_. Da er det `Common.Logging.Log4Net1213` som skal installeres. 
2. Legg merke til hvilken versjon av Log4net som faktisk installeres. Av en eller annen grunn kan det bli 2.0.0 som installeres. Da må versjonen oppdateres til 2.0.3.

En fullstendig App.config med Log4Net-adapter og en `RollingFileAppender`:

{% highlight xml %}
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <sectionGroup name="common">
      <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
    </sectionGroup>
    <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
  </configSections>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
  <common>
    <logging>
      <factoryAdapter type="Common.Logging.Log4Net.Log4NetLoggerFactoryAdapter, Common.Logging.Log4net1213">
        <arg key="configType" value="INLINE" />
      </factoryAdapter>
    </logging>
  </common>
  <log4net>
    <logger name="Difi.Oppslagstjeneste.Klient">
      <appender-ref ref="DebugRollingFileAppender" />
      <level value="DEBUG" />
    </logger>
    <appender name="DebugRollingFileAppender" type="log4net.Appender.RollingFileAppender">
      <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
      <file value="${AppData}\Difi\Log\" />
      <appendToFile value="true" />
      <rollingStyle value="Date" />
      <staticLogFileName value="false" />
      <rollingStyle value="Composite" />
      <param name="maxSizeRollBackups" value="10" />
      <datePattern value="yyyy.MM.dd' Difi.Oppslagstjeneste-klient-dotnet.log'" />
      <maximumFileSize value="100MB" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5lev - %message%newline" />
      </layout>
    </appender>
  </log4net>
</configuration>
{% endhighlight %}


<h3 id="loggeforsporselogrespons"> Logge forespørsel og respons</h3>

Det er mulig å logge hele forespørsel/respons som blir sendt/mottatt. For å aktivere dette setter du følgende på <code>OppslagstjenesteKonfigurasjon</code>:

{% highlight csharp %}
OppslagstjenesteKonfigurasjon.LoggForespørselOgRespons = true;
{% endhighlight%}

<blockquote> Merk at logging av forespørsel og respons kan gi mye dårligere ytelse. Det er ingen grunn til å logge dette i et produksjonsmiljø.</blockquote>
