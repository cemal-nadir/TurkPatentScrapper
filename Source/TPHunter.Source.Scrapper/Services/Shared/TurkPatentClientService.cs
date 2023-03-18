using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;
using TPHunter.Source.Scrapper.Abstract.Shared;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class TurkPatentClientService : ITurkPatentClientService
    {
        private readonly HttpClient _httpClient;

        private const string ApiTrademarkUri =
            "https://www.turkpatent.gov.tr/api/news?locale=tr&category=marka&limit=1";

        private const string ApiPatentUri =
            "https://www.turkpatent.gov.tr/api/news?locale=tr&category=patent-faydali-model&limit=1";

        private const string ApiDesignUri = "https://www.turkpatent.gov.tr/api/news?locale=tr&category=tasarim&limit=1";

        public TurkPatentClientService()
        {
            _httpClient = TPHunter.Shared.Scrapper.Ioc.Resolve<IApiClient>(nameof(TurkPatentClient)).Client;
        }
        public async Task<ISearchParam> GetTrademarkParam()
        {
            var lastBulletin = await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiTrademarkUri);
            if (lastBulletin?.Payloads?.Datas?.FirstOrDefault() == null) return null;
            var title = lastBulletin.Payloads.Datas.FirstOrDefault()!.Title;
            return title != null
                ? new SearchParam()
                    { BulletinNumber = int.Parse(title) }
                : null;
        }
        public async Task<ISearchParam> GetDesignParam()
        {
            var lastBulletin = await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiDesignUri);
            if (lastBulletin?.Payloads?.Datas?.FirstOrDefault() == null) return null;
            var title = lastBulletin.Payloads.Datas.FirstOrDefault()!.Title;
            return title != null
                ? new SearchParam()
                { BulletinNumber = int.Parse(title) }
                : null;
        }
        public async Task<ISearchParam> GetPatentParam()
        {
            var lastBulletin = await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiPatentUri);
            if (lastBulletin?.Payloads?.Datas?.FirstOrDefault() == null) return null;
            var bulletinArray = lastBulletin.Payloads.Datas.FirstOrDefault()!.Title.Split('_').Select(int.Parse).ToArray();

            var bulletinStartDate = new DateTime(bulletinArray[0], bulletinArray[1], 1);
            var bulletinEndDate = new DateTime(bulletinArray[0], bulletinArray[1],
                bulletinArray[0] == DateTime.Now.Year && bulletinArray[1] == DateTime.Now.Month
                    ? DateTime.Now.Day
                    : DateTime.DaysInMonth(bulletinArray[0], bulletinArray[1]));
            return new SearchParam()
            {
                StartDate = bulletinStartDate,
                EndDate = bulletinEndDate
            };

        }
        private class TurkPatentResponseReceiveModel
        {
            [JsonPropertyName("payload")]
            public Payload Payloads { get; set; }

          
          

            public class Payload
            {
                [JsonPropertyName("data")]
                // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
                public IEnumerable<Data> Datas { get; set; }
            }
            public class Data
            {
                [JsonPropertyName("title")] public string Title { get; set; }
            }
        }

    }




}
