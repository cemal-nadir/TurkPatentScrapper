using System.Net.Http;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Handlers
{
    public class ApiClient:IApiClient
    {
        private HttpClient _httpClient;
        public HttpClient Client => _httpClient ??= new HttpClient(new ClientCredentialTokenHandler());
    }
}
