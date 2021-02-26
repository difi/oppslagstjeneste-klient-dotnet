using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Difi.Oppslagstjeneste.Klient.Handlers
{
    internal class RequestHeaderHandler : DelegatingHandler
    {
        public RequestHeaderHandler()
            : base(new HttpClientHandler())
        {
        }

        public RequestHeaderHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("User-Agent", GetAssemblyVersion());
            request.Headers.Add("SOAPAction", "\"\"");
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
        }

        private static string GetAssemblyVersion()
        {
            var netVersion = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies().First(x => x.Name == "netstandard").Version.ToString();

            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

            return $"difi-oppslagstjeneste-klient/{assemblyVersion} (.NET/{netVersion})";
        }
    }
}
