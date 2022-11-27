using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.DataSaver.DI;

namespace TPHunter.Source.DataSaver.Concrete
{
    public class ScrapperClientService<TRequest> : IScrapperClientService<TRequest> where TRequest : class
    {
        private readonly HttpClient _httpClient;

        public ScrapperClientService(string apiUri)
        {
            Shared.Scrapper.Ioc.ApiClientFactory();
            _httpClient = Ioc.Resolve<IApiClient>().Client;
            _httpClient.BaseAddress = new Uri(apiUri);
        }

        public async Task<IEnumerable<string>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam)
        {
            var response = await _httpClient.PostAsJsonAsync("GetLastPulledApplicationNumbers", searchParam);

           

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();

        }

        public async Task<IEnumerable<Guid>> GetLastPulledIdsAsync(ISearchParam searchParam)
        {
            var response = await _httpClient.PostAsJsonAsync("GetLastPulledIds", searchParam);



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<Guid>>();
        }

        public async Task<int> GetLastPulledCountAsync(ISearchParam searchParam)
        {
            var response = await _httpClient.PostAsJsonAsync("GetLastPulledCount", searchParam);

           

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task InsertAsync(TRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync(default(string), model);

           

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

        }

        public async Task RemoveAsync(Guid ıd)
        {
            var response = await _httpClient.DeleteAsync(ıd.ToString());

           

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");
        }

        public async Task UpdateAsync(TRequest model)
        {
            var response = await _httpClient.PutAsJsonAsync(default(string), model);

           

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");
        }
    }
}
