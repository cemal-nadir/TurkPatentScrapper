using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Shared.Scrapper.Handlers
{
    internal class ClientCredentialTokenService:IClientCredentialTokenService
    {
        private readonly LiveConfig _config;
        private readonly HttpClient _httpClient;
        public ClientCredentialTokenService()
        {
            _config=new LiveConfig();
            Ioc.HttpClientFactory();
            _httpClient = Ioc.Resolve<IApiClient>().Client;
        }
        public async Task<string> GetToken()
        {
            var currentToken = _config.GetAccessToken();
            var currentTokenExpiration=_config.GetAccessTokenExpiration();
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
                ClientId = RuntimeConfigs.GeneralConfig.ServicesConfigs.ScrapApiClient,
                ClientSecret = RuntimeConfigs.GeneralConfig.ServicesConfigs.ScrapApiSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            _config.SetAccessToken(newToken.AccessToken, DateTimeOffset.FromUnixTimeSeconds(newToken.ExpiresIn).Date);

            return newToken.AccessToken;

        }
    }
}
