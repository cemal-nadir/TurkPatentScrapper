using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Shared.Scrapper.Handlers
{
    internal class ClientCredentialTokenService:IClientCredentialTokenService
    {
        private readonly HttpClient _httpClient;
        public ClientCredentialTokenService()
        {
            _httpClient = Ioc.Resolve<IApiClient>(nameof(IdentityClient)).Client;
        }
        public async Task<string> GetToken()
        {
            var currentToken = LiveConfigFunctions.GetAccessToken();
            var currentTokenExpiration=LiveConfigFunctions.GetAccessTokenExpiration();
            if (!string.IsNullOrEmpty(currentToken) && currentTokenExpiration > DateTime.Now)
                return currentToken;

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = RuntimeConfigs.GeneralConfig.Services.IdentityApiUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = RuntimeConfigs.GeneralConfig.ServiceConfig.ScrapApiClient,
                ClientSecret = RuntimeConfigs.GeneralConfig.ServiceConfig.ScrapApiSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            LiveConfigFunctions.SetAccessToken(newToken.AccessToken, DateTime.Now.AddSeconds(newToken.ExpiresIn));

            return newToken.AccessToken;

        }
    }
}
