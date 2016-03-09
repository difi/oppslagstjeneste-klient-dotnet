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
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.XmlValidering;

namespace Difi.Oppslagstjeneste.Klient
{
    internal class OppslagstjenesteHelper
    {
        private readonly OppslagstjenesteInstillinger _oppslagstjenesteInstillinger;

        public OppslagstjenesteHelper(OppslagstjenesteKonfigurasjon konfigurasjon, OppslagstjenesteInstillinger oppslagstjenesteInstillinger)
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
            using (var client = Client())
            {
                SetDefaultRequestHeaders(client);
                ValidateRequest(envelope);
                var requestXml = envelope.ToXml();
                Logger.Log(TraceEventType.Verbose, requestXml.OuterXml);
                var stringContent = new StringContent(requestXml.InnerXml, Encoding.UTF8, "application/soap+xml");
                using (var response = await client.PostAsync(OppslagstjenesteKonfigurasjon.Miljø.Url, stringContent))
                {
                    var soapResponse = await response.Content.ReadAsStreamAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        CheckResponseForErrors(soapResponse);
                    }
                    var responseDocoument = new ResponseDokument(soapResponse);
                    ValidateResponse(envelope, responseDocoument);
                    Logger.Log(TraceEventType.Verbose, responseDocoument.Envelope.InnerXml);
                    return responseDocoument.ToDtoObject<T>();
                }
            }
        }

        internal virtual HttpClient Client()
        {
            var httpClientHandler = HttpClientHandler();
            return new HttpClient(httpClientHandler);
        }

        private HttpClientHandler HttpClientHandler()
        {
            var httpClientHandler = new HttpClientHandler();
            if (OppslagstjenesteKonfigurasjon.BrukProxy)
            {
                httpClientHandler = new HttpClientHandler
                {
                    Proxy = Proxy()
                };
            }
            return httpClientHandler;
        }

        private static void CheckResponseForErrors(Stream soapResponse)
        {
            var reader = new StreamReader(soapResponse);
            var text = reader.ReadToEnd();
            var exception = new SoapException(text);
            Logger.Log(TraceEventType.Critical,
                $"> Feil ved sending (Skyldig: {exception.Skyldig})");
            Logger.Log(TraceEventType.Critical, $"  - {exception.Beskrivelse}");
            throw exception;
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

        private void ValidateResponse(AbstractEnvelope envelope, ResponseDokument responseDokument)
        {
            var responsvalidator = new Oppslagstjenestevalidator(envelope.ToXml(), responseDokument, _oppslagstjenesteInstillinger, OppslagstjenesteKonfigurasjon.Miljø as Miljø);
            responsvalidator.Valider();
        }

        private static void ValidateRequest(AbstractEnvelope envelope)
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