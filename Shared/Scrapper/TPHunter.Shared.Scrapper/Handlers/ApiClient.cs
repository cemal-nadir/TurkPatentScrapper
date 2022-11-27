using System.Net.Http;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Handlers
{
    public class ApiClient:IApiClient
    {
        private HttpClient _httpClient;
        private ClientCredentialTokenHandler _clientCredentialTokenHandler;

        public HttpClient Client => _httpClient ??= new HttpClient(ClientCredentialTokenHandler);
        private ClientCredentialTokenHandler ClientCredentialTokenHandler=> _clientCredentialTokenHandler??=new ClientCredentialTokenHandler();
    }
}
