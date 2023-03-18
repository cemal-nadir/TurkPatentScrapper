using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TPHunter.Shared.ApiUtility.ControllerBases.Dtos;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;
using TPHunter.Source.DataSaver.Abstract;

namespace TPHunter.Source.DataSaver.Concrete
{
    public class ScrapperClientService : IScrapperClientService<IModel>
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUri;
        public ScrapperClientService(string apiUri)
        {
            _httpClient = Shared.Scrapper.Ioc.Resolve<IApiClient>(nameof(ApiClient)).Client;
            _apiUri = apiUri;
        }

        public async Task<Response<IEnumerable<string>>> GetLastPulledApplicationNumbersAsync(ISearchParam searchParam)
        {

            var response = await _httpClient.PostAsJsonAsync($"{_apiUri}GetLastPulledApplicationNumbers", searchParam).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<Response<IEnumerable<string>>>().ConfigureAwait(false);

        }

        public async Task<Response<IEnumerable<Guid>>> GetLastPulledIdsAsync(ISearchParam searchParam)
        {

            var response = await _httpClient.PostAsJsonAsync($"{_apiUri}GetLastPulledIds", searchParam);



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<Response<IEnumerable<Guid>>>();


        }

        public async Task<Response<int>> GetLastPulledCountAsync(ISearchParam searchParam)
        {

            var response = await _httpClient.PostAsJsonAsync($"{_apiUri}GetLastPulledCount", searchParam);



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<Response<int>>();


        }

        public async Task InsertAsync(IModel model)
        {

            var response = await _httpClient.PostAsJsonAsync(_apiUri, model);



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

        }

        public async Task RemoveAsync(Guid ıd)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUri}{ıd.ToString()}");



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");
        }

        public async Task UpdateAsync(IModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUri}", model);



            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");
        }
    }
}
