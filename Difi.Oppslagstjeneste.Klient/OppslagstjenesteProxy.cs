using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Difi.Felles.Utility;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.XmlValidering;

namespace Difi.Oppslagstjeneste.Klient
{
    internal class OppslagstjenesteProxy
    {
        private readonly OppslagstjenesteInstillinger _oppslagstjenesteInstillinger;

        public OppslagstjenesteProxy(OppslagstjenesteKonfigurasjon konfigurasjon, OppslagstjenesteInstillinger oppslagstjenesteInstillinger)
        {
            _oppslagstjenesteInstillinger = oppslagstjenesteInstillinger;
            OppslagstjenesteKonfigurasjon = konfigurasjon;
        }

        public OppslagstjenesteKonfigurasjon OppslagstjenesteKonfigurasjon { get; }

        private static string NetVersion
        {
            get
            {
                var netVersion = Assembly
                    .GetExecutingAssembly()
                    .GetReferencedAssemblies().First(x => x.Name == "System.Core").Version.ToString();
                return netVersion;
            }
        }

        private static Version AssemblyVersion
        {
            get
            {
                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                return assemblyVersion;
            }
        }

        public async Task<T> SendAsync<T>(AbstractEnvelope envelope)
        {
            var httpClientHandler = new HttpClientHandler();
            if (OppslagstjenesteKonfigurasjon.BrukProxy)
            {
                httpClientHandler = new HttpClientHandler
                {
                    Proxy = Proxy()
                };
            }
            
            using (var client = new HttpClient(httpClientHandler))
            {
                SetDefaultRequestHeaders(client);
                ValiderForespørsel(envelope);
                try
                {
                    var requestXml = envelope.ToXml();
                    Logger.Log(TraceEventType.Verbose, requestXml.OuterXml);
                    var stringContent = new StringContent(requestXml.InnerXml, Encoding.UTF8, "application/soap+xml");
                    using (var response = await client.PostAsync(OppslagstjenesteKonfigurasjon.Miljø.Url, stringContent))
                    {
                        var soapResponse = await response.Content.ReadAsStreamAsync();
                        var responsvalidator = ValiderRespons(envelope, soapResponse);
                        Logger.Log(TraceEventType.Verbose, responsvalidator.MottattDokument.InnerXml);
                        return SerializeUtil.Deserialize<T>(responsvalidator.Body);
                    }
                }
                catch (WebException we)
                {
                    using (var stream = we.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        var error = reader.ReadToEnd();
                        var exception = new SoapException(error);
                        Logger.Log(TraceEventType.Critical,
                            $"> Feil ved sending (Skyldig: {exception.Skyldig})");
                        Logger.Log(TraceEventType.Critical, $"  - {exception.Beskrivelse}");
                        throw exception;
                    }
                }
            }
        }

        private WebProxy Proxy()
        {
            return new WebProxy(
                new UriBuilder(OppslagstjenesteKonfigurasjon.ProxyScheme,
                    OppslagstjenesteKonfigurasjon.ProxyHost, OppslagstjenesteKonfigurasjon.ProxyPort).Uri);
        }

        private static void SetDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("SOAPAction", "\"\"");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", string.Format("DifiOppslagstjeneste/{1} (.NET/{0})", NetVersion, AssemblyVersion));
        }

        private Oppslagstjenestevalidator ValiderRespons(AbstractEnvelope envelope, Stream soapResponse)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(soapResponse);
            var responsvalidator = new Oppslagstjenestevalidator(envelope.ToXml(), xmlDocument, _oppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.Miljø as Miljø);
            responsvalidator.Valider();
            return responsvalidator;
        }

        private static void ValiderForespørsel(AbstractEnvelope envelope)
        {
            var xml = envelope.ToXml();
            var xmlValidator = new OppslagstjenesteXmlvalidator();
            var xmlValidert = xmlValidator.ValiderDokumentMotXsd(xml.OuterXml);
            if (!xmlValidert)
            {
                throw new XmlException(xmlValidator.ValideringsVarsler);
            }
        }
    }
}