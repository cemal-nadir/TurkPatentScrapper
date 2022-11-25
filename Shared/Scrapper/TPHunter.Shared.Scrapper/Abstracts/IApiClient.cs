using System.Net.Http;

namespace TPHunter.Shared.Scrapper.Abstracts
{
    public interface IApiClient
    {
        public HttpClient Client { get; }
    }
}
