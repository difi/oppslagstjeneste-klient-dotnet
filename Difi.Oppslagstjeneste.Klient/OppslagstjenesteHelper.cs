using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Difi.Oppslagstjeneste.Klient.Domene.Exceptions;
using Difi.Oppslagstjeneste.Klient.Envelope;
using Difi.Oppslagstjeneste.Klient.Handlers;
using Difi.Oppslagstjeneste.Klient.Svar;
using Difi.Oppslagstjeneste.Klient.XmlValidering;
using log4net;

namespace Difi.Oppslagstjeneste.Klient
{
    internal class OppslagstjenesteHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient _httpClient;

        public OppslagstjenesteHelper(OppslagstjenesteKonfigurasjon konfigurasjon)
        {
            OppslagstjenesteKonfigurasjon = konfigurasjon;
            _httpClient = new HttpClient(HttpClientHandlerChain());
        }

        public OppslagstjenesteKonfigurasjon OppslagstjenesteKonfigurasjon { get; }

        public async Task<ResponseContainer> SendAsync(AbstractEnvelope envelope)
        {
            ValidateRequest(envelope);
            var requestXml = envelope.XmlDocument;
            var stringContent = new StringContent(requestXml.InnerXml, Encoding.UTF8, "application/soap+xml");
            using (var response = await Client().PostAsync(OppslagstjenesteKonfigurasjon.Miljø.Url, stringContent))
            {
                var soapResponse = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warn($"Response HTTP code was {response.StatusCode}");
                    CheckResponseForErrors(soapResponse);
                }
                return new ResponseContainer(soapResponse);
            }
        }

        internal virtual HttpClient Client()
        {
            return _httpClient;
        }

        private HttpMessageHandler HttpClientHandlerChain()
        {
            HttpClientHandler httpClientHandler;
            if (!string.IsNullOrEmpty(OppslagstjenesteKonfigurasjon.ProxyHost))
            {
                httpClientHandler = new HttpClientHandler
                {
                    Proxy = CreateProxy()
                };
            }
            else
            {
                httpClientHandler = new HttpClientHandler();
            }

            return new RequestHeaderHandler(httpClientHandler);
        }

        private static void CheckResponseForErrors(Stream soapResponse)
        {
            var reader = new StreamReader(soapResponse);
            var text = reader.ReadToEnd();
            var exception = new SoapException(text);
            Log.Error($"> Feil ved sending (Skyldig: {exception.Skyldig})");
            Log.Error($"  - {exception.Beskrivelse}");
            throw exception;
        }

        private WebProxy CreateProxy()
        {
            return new WebProxy(
                new UriBuilder(OppslagstjenesteKonfigurasjon.ProxyScheme,
                    OppslagstjenesteKonfigurasjon.ProxyHost, OppslagstjenesteKonfigurasjon.ProxyPort).Uri);
        }

        private static void ValidateRequest(AbstractEnvelope envelope)
        {
            var xml = envelope.XmlDocument;
            var xmlValidator = new OppslagstjenesteXmlvalidator();
            var xmlValidert = xmlValidator.ValiderDokumentMotXsd(xml.OuterXml);
            if (!xmlValidert)
            {
                Log.Warn($"Request did not validate OK. Errors[{xmlValidator.ValideringsVarsler}]");
                throw new XmlException(xmlValidator.ValideringsVarsler);
            }
        }
    }
}