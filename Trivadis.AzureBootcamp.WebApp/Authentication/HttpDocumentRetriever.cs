using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Trivadis.AzureBootcamp.CrossCutting.Logging;

namespace Trivadis.AzureBootcamp.WebApp.Authentication
{
    internal class HttpDocumentRetriever : IDocumentRetriever
    {
        private readonly ILogger _log = LogManager.GetLogger(typeof(HttpDocumentRetriever));

        private readonly HttpClient _httpClient;

        public HttpDocumentRetriever()
            : this(new HttpClient())  { }

        public HttpDocumentRetriever(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new ArgumentNullException("httpClient");

            _httpClient = httpClient;
        }

        public async Task<string> GetDocumentAsync(string address, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException("address");

            try
            {
                _log.Debug("Get document {0}..", address);

                HttpResponseMessage response = await _httpClient.GetAsync(address, cancel).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                _log.Debug("{0}", result);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to get document from: " + address, ex);
            }
        }
    }
}