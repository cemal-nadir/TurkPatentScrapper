using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TPHunter.Source.Core.Configs
{
    public class LiveConfig
    {
        public TrademarkSettings TrademarkSettings { get; set; }
        public DesignSettings DesignSettings { get; set; }
        public PatentSettings PatentSettings { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpirationDate { get; set; }
        public LiveConfig GetConfig()
        {
            if (File.Exists(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json"))
            {
                using var streamReader = new StreamReader(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
                return JsonConvert.DeserializeObject<LiveConfig>(streamReader.ReadToEndAsync().Result);
            }

            LiveConfig config = new()
            {
                TrademarkSettings = new TrademarkSettings()
                {
                    CurrentBulletin = 1,
                    ScrapTimings = new ScrapTimings()
                    {
                        DownloadHourDelay = 0,
                        DownloadMinuteDelay = 15,
                        DownloadSecondDelay = 0,
                        LastScrapTime = DateTime.MinValue,
                        UpdateHourDelay = 24,
                        UpdateMinuteDelay = 0,
                        UpdateSecondDelay = 0

                    },
                    UpdateConditions = new UpdateConditions()
                    {
                        UpdateAttorneyNames = new List<string>()
                        {
                            "Uğur Bilen"
                        }
                    }
                },
                DesignSettings = new DesignSettings()
                {
                    CurrentBulletin = 1,
                    ScrapTimings = new ScrapTimings()
                    {
                        DownloadHourDelay = 24,
                        DownloadSecondDelay = 0,
                        DownloadMinuteDelay = 0,
                        LastScrapTime = DateTime.MinValue,
                        UpdateHourDelay = 168,
                        UpdateMinuteDelay = 0,
                        UpdateSecondDelay = 0

                    },
                    UpdateConditions = new UpdateConditions()
                    {
                        UpdateAttorneyNames = new List<string>()
                        {
                            "Uğur Bilen"
                        }
                    }
                },
                PatentSettings = new PatentSettings()
                {
                    CurrentPulledDate = new DateTime(1997, 1, 1),
                    ScrapTimings = new ScrapTimings()
                    {
                        DownloadHourDelay = 24,
                        DownloadMinuteDelay = 0,
                        DownloadSecondDelay = 0,
                        LastScrapTime = DateTime.MinValue,
                        UpdateHourDelay = 168,
                        UpdateMinuteDelay = 0,
                        UpdateSecondDelay = 0
                    },
                    UpdateConditions = new UpdateConditions()
                    {
                        UpdateAttorneyNames = new List<string>()
                        {
                            "Uğur Bilen"
                        }
                    }
                },
                AccessToken = string.Empty,
                TokenExpirationDate = DateTime.Now
            };
            using var streamWriter = File.CreateText(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
            return GetConfig();

        }

        public string GetAccessToken() => GetConfig().AccessToken;
        public DateTime GetAccessTokenExpiration() => GetConfig().TokenExpirationDate;
        public void SetAccessToken(string token, DateTime expirationDate)
        {
            var config = GetConfig();
            config.AccessToken = token;
            config.TokenExpirationDate = expirationDate;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
    }

    #region Settings
    public class TrademarkSettings
    {
        public int CurrentBulletin { get; set; }
        public UpdateConditions UpdateConditions { get; set; }
        public ScrapTimings ScrapTimings { get; set; }
    }
    public class DesignSettings
    {
        public int CurrentBulletin { get; set; }
        public UpdateConditions UpdateConditions { get; set; }
        public ScrapTimings ScrapTimings { get; set; }
    }
    public class PatentSettings
    {
        public DateTime CurrentPulledDate { get; set; }
        public UpdateConditions UpdateConditions { get; set; }
        public ScrapTimings ScrapTimings { get; set; }
    }

    public class UpdateConditions
    {
        public IEnumerable<string> UpdateHolderCodes { get; set; }
        public IEnumerable<string> UpdateAttorneyNames { get; set; }

    }

    public class ScrapTimings
    {
        public int DownloadHourDelay { get; set; }
        public int DownloadMinuteDelay { get; set; }
        public int DownloadSecondDelay { get; set; }
        public int UpdateHourDelay { get; set; }
        public int UpdateMinuteDelay { get; set; }
        public int UpdateSecondDelay { get; set; }
        public DateTime LastScrapTime { get; set; }
    }


    #endregion


}
