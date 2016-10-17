---
identifier: Logging
title: Logging
layout: default
---

<h3 id="genLogging">Generelt om logging</h3>
Loggrammeverket som blir brukt er Log4Net 2.0.4. Pass på å bruke 2.0.4 eller høyere for at logging skal fungere.

Hvis man setter loggnivået til `DEBUG` vil alle forespørsler og responser bli logget. `WARN` vil kun logge feilmeldinger. Alle loggerne ligger i navnerommet `Difi.Oppslagstjeneste.Klient`.

{% highlight xml %}

<log4net>
 <logger name="Difi.Oppslagstjeneste.Klient">
   <appender-ref ref="TraceAppender"/>
   <appender-ref ref="RollingFileAppender"/>
   <level value="WARN"/>
 </logger>
 <appender name="TraceAppender" type="log4net.Appender.TraceAppender">
   <layout type="log4net.Layout.PatternLayout">
     <conversionPattern value="%date [%thread] %-5p %c %message%newline" />
   </layout>
 </appender>
 <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
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
     <conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
   </layout>
 </appender>
</log4net>

{% endhighlight %}

<h3 id="loggingrequestresponse">Eksempel på logging av forespørsel og respons</h3>

Det kan være kjekt å logge selve SOAP forespørselen/responsen som blir sendt/mottatt når man gjør den initielle integrasjonen. Dette kan man gjøre ved å lage en logger mot navnerommet `Difi.Oppslagstjeneste.Klient.RequestLog`. Se følgende eksempel:

{% highlight xml %}

<log4net>
 <logger name="Difi.Oppslagstjeneste.Klient.RequestLog">
   <appender-ref ref="TraceAppender"/>
   <appender-ref ref="RollingFileAppender"/>
   <level value="DEBUG"/>
 </logger>
 <appender name="TraceAppender" type="log4net.Appender.TraceAppender">
   <layout type="log4net.Layout.PatternLayout">
     <conversionPattern value="%date [%thread] %-5p %c %message%newline" />
   </layout>
 </appender>
 <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
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
     <conversionPattern value="%date [%thread] %-5level %logger [%property{NDC}] - %message%newline" />
   </layout>
 </appender>
</log4net>

{% endhighlight %}


<blockquote>
Advarsel: Forespørsel- og response logging burde ikke være aktivert i et produksjonsmiljø siden dette kan ha innvirkninger på ytelsen av applikasjonen.
</blockquote>


