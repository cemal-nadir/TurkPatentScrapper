using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;

namespace TPHunter.Source.DataSaver.Concrete
{
    public class ScrapperClientHelperService:IScrapperClientHelperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _getAttorneyIdsByNamesUri = $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Attorney?";
        private readonly string _getFilteredTrademarkApplicationNumbersUri = $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Trademark/GetFilteredApplicationNumbers?";
        private readonly string _getFilteredPatentApplicationNumbersUri = $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Patent/GetFilteredApplicationNumbers?";
        private readonly string _getFilteredDesignApplicationNumbersUri = $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Design/GetFilteredApplicationNumbers?";
        public ScrapperClientHelperService()
        {
            _httpClient = Ioc.Resolve<IApiClient>(nameof(ApiClient)).Client;
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
            var requestUri =PrepareUrlQueryString(attorneyNames, nameof(attorneyNames), _getAttorneyIdsByNamesUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<Guid>>();
        }

        public async Task<IEnumerable<string>> GetFilteredTrademarkApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = _getFilteredTrademarkApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x=>x.ToString()).ToArray(), nameof(attorneyIds), requestUri)+"&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        public async Task<IEnumerable<string>> GetFilteredPatentApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = _getFilteredPatentApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x => x.ToString()).ToArray(), nameof(attorneyIds), requestUri) + "&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        public async Task<IEnumerable<string>> GetFilteredDesignApplicationNumbers(Guid[] attorneyIds, string[] holderCodes)
        {
            var requestUri = _getFilteredDesignApplicationNumbersUri;
            requestUri = PrepareUrlQueryString(attorneyIds.Select(x => x.ToString()).ToArray(), nameof(attorneyIds), requestUri) + "&";
            requestUri = PrepareUrlQueryString(holderCodes, nameof(holderCodes), requestUri);

            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"api error code => {response.StatusCode}");

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}
