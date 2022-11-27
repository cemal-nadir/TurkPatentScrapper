using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;

namespace TPHunter.Source.DataSaver.Concrete
{
    public class ScrapperClientHelperService:IScrapperClientHelperService
    {
        private readonly HttpClient _httpClient;
        private const string GetAttorneyIdsByNamesUri = "/Api/Attorney?";
        private const string GetFilteredTrademarkApplicationNumbersUri = "/Api/Trademark/GetFilteredApplicationNumbers?";
        private const string GetFilteredPatentApplicationNumbersUri = "/Api/Patent/GetFilteredApplicationNumbers?";
        private const string GetFilteredDesignApplicationNumbersUri = "/Api/Design/GetFilteredApplicationNumbers?";
        public ScrapperClientHelperService()
        {
            Ioc.ApiClientFactory();
            _httpClient = Ioc.Resolve<IApiClient>().Client;
            _httpClient.BaseAddress = new Uri(RuntimeConfigs.GeneralConfig.Services.ScrapApiUri);
        }

        private static string PrepareUrlQueryString(IReadOnlyList<string> values, string valueName,string requestUri)
        {
            for (var i = 0; i < values.Count; i++)
            {
                requestUri = $"{requestUri}{valueName}[{i}]={values[i]}&";
            }

            requestUri = requestUri[..^1];
            return requestUri;
        }
        public async Task<IEnumerable<Guid>> GetAttorneyIdsByNames(string[] attorneyNames)
        {
            var requestUri =PrepareUrlQueryString(attorneyNames, nameof(attorneyNames), GetAttorneyIdsByNamesUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<Guid>>();
        }

        public async Task<IEnumerable<string>> GetFilteredTrademarkApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = GetFilteredTrademarkApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x=>x.ToString()).ToArray(), nameof(attorneyIds), requestUri)+"&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        public async Task<IEnumerable<string>> GetFilteredPatentApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = GetFilteredPatentApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x => x.ToString()).ToArray(), nameof(attorneyIds), requestUri) + "&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        public async Task<IEnumerable<string>> GetFilteredDesignApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = GetFilteredDesignApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x => x.ToString()).ToArray(), nameof(attorneyIds), requestUri) + "&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}
