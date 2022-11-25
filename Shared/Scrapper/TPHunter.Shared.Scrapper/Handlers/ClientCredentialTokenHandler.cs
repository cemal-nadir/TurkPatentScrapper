using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;

namespace TPHunter.Shared.Scrapper.Handlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;
        public ClientCredentialTokenHandler()
        {
            Ioc.ClientCredentialTokenFactory();
            _clientCredentialTokenService = Ioc.Resolve<IClientCredentialTokenService>();
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetToken());

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
