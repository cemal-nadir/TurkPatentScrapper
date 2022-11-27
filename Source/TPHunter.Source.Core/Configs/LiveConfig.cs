using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TPHunter.Source.Core.Configs
{
    public class LiveConfig
    {
        public TrademarkSettings TrademarkSettings { get; private init; }
        public DesignSettings DesignSettings { get; private init; }
        public PatentSettings PatentSettings { get; private init; }
        private string AccessToken { get; set; }
        private DateTime TokenExpirationDate { get; set; }
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
                        LastDownload = DateTime.MinValue,
                        LastUpload = DateTime.MinValue,
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
                        LastDownload = DateTime.MinValue,
                        LastUpload = DateTime.MinValue,
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
                        LastDownload = DateTime.MinValue,
                        LastUpload = DateTime.MinValue,
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
        public void SetTradeMarkCurrentBulletin(int bulletin)
        {
            var config = GetConfig();
            config.TrademarkSettings.CurrentBulletin = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void SetDesignCurrentBulletin(int bulletin)
        {
            var config = GetConfig();
            config.DesignSettings.CurrentBulletin = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void SetPatentCurrentBulletin(DateTime bulletin)
        {
            var config = GetConfig();
            config.PatentSettings.CurrentPulledDate = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }

        public void UpdateTrademarkLastDownload()
        {
            var config = GetConfig();
            config.TrademarkSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void UpdateTrademarkLastUpload()
        {
            var config = GetConfig();
            config.TrademarkSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void UpdatePatentLastDownload()
        {
            var config = GetConfig();
            config.PatentSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void UpdatePatentLastUpload()
        {
            var config = GetConfig();
            config.PatentSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void UpdateDesignLastDownload()
        {
            var config = GetConfig();
            config.DesignSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public void UpdateDesignLastUpload()
        {
            var config = GetConfig();
            config.DesignSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }

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
        public DateTime LastDownload { get; set; }
        public DateTime LastUpload { get; set; }
    }


    #endregion



