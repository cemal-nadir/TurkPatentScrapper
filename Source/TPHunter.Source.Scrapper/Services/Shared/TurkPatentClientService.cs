using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Scrapper.Abstract.Shared;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class TurkPatentClientService:ITurkPatentClientService
    {
        private readonly HttpClient _httpClient;

        private const string ApiTrademarkUri =
            "https://www.turkpatent.gov.tr/api/news?locale=tr&category=marka&limit=1";

        private const string ApiPatentUri =
            "https://www.turkpatent.gov.tr/api/news?locale=tr&category=patent-faydali-model&limit=1";

        private const string ApiDesignUri = "https://www.turkpatent.gov.tr/api/news?locale=tr&category=tasarim&limit=1";

        public TurkPatentClientService()
        {
            TPHunter.Shared.Scrapper.Ioc.TurkPatentClientFactory();

            _httpClient = TPHunter.Shared.Scrapper.Ioc.Resolve<IApiClient>().Client;
        }
        public async Task<ISearchParam> GetTrademarkParam()
        {
            var lastBulletin = (await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiTrademarkUri))
                ?.Datas.FirstOrDefault()?.Title;
            return lastBulletin is null ? null : new BulletinParam() { BulletinNumber = int.Parse(lastBulletin) };
        }
        public async Task<ISearchParam> GetDesignParam()
        {
            var lastBulletin = (await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiDesignUri))
                ?.Datas.FirstOrDefault()?.Title;
            return lastBulletin is null ? null : new BulletinParam() { BulletinNumber = int.Parse(lastBulletin) };
        }
        public async Task<ISearchParam> GetPatentParam()
        {
            var lastBulletin = (await _httpClient.GetFromJsonAsync<TurkPatentResponseReceiveModel>(ApiPatentUri))
                ?.Datas.FirstOrDefault()?.Title;
            if (lastBulletin is null) return null;
            var bulletinArray = lastBulletin.Split('_').Select(int.Parse).ToArray();

            var bulletinStartDate = new DateTime(bulletinArray[0], bulletinArray[1], 1);
            var bulletinEndDate = new DateTime(bulletinArray[0], bulletinArray[1],
                bulletinArray[0] == DateTime.Now.Year && bulletinArray[1] == DateTime.Now.Month
                    ? DateTime.Now.Day
                    : DateTime.DaysInMonth(bulletinArray[0], bulletinArray[1]));
            return new DateRangeParam()
            {
                StartDate = bulletinStartDate,
                EndDate = bulletinEndDate
            };
        }
        private class TurkPatentResponseReceiveModel
        {
            public TurkPatentResponseReceiveModel(IEnumerable<Data> datas)
            {
                Datas = datas;
            }

            [JsonPropertyName("data")]
            public IEnumerable<Data> Datas { get; }
            public abstract class Data
            {
                protected Data(string title)
                {
                    Title = title;
                }

                [JsonPropertyName("title")]
                public string Title { get; }
            }
        }

    }

    

   
}
