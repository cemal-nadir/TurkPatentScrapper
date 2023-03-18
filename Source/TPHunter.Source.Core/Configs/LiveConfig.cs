using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace TPHunter.Source.Core.Configs
{
    public class LiveConfig
    {
        public TrademarkSettings TrademarkSettings { get; set; }
        public DesignSettings DesignSettings { get; set; }
        public PatentSettings PatentSettings { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }

    #region Methods
    public static class LiveConfigFunctions
    {
        public static LiveConfig GetConfig()
        {
            if (File.Exists(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json"))
            {
                while (true)
                {
                    try
                    {
                        using var streamReader = new StreamReader(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
                        return JsonConvert.DeserializeObject<LiveConfig>(streamReader.ReadToEndAsync().Result);
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                }
               
            }

            LiveConfig config = new()
            {
                TrademarkSettings = new TrademarkSettings()
                {
                    CurrentBulletin = 0,
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
                    CurrentBulletin = 0,
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
            using var streamWriter = File.CreateText(RuntimeConfigs.ApplicationStartupPath + "liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
            streamWriter.Close();
            return GetConfig();

        }

        public static string GetAccessToken() => GetConfig().AccessToken;
        public static DateTime GetAccessTokenExpiration() => GetConfig().TokenExpirationDate;

        public static void SetAccessToken(string token, DateTime expirationDate)
        {
            var config = GetConfig();
            config.AccessToken = token;
            config.TokenExpirationDate = expirationDate;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
            streamWriter.Close();
        }
        public static void SetTradeMarkCurrentBulletin(int bulletin)
        {
            var config = GetConfig();
            config.TrademarkSettings.CurrentBulletin = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void SetDesignCurrentBulletin(int bulletin)
        {
            var config = GetConfig();
            config.DesignSettings.CurrentBulletin = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void SetPatentCurrentBulletin(DateTime bulletin)
        {
            var config = GetConfig();
            config.PatentSettings.CurrentPulledDate = bulletin;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }

        public static void UpdateTrademarkLastDownload()
        {
            var config = GetConfig();
            config.TrademarkSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void UpdateTrademarkLastUpload()
        {
            var config = GetConfig();
            config.TrademarkSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void UpdatePatentLastDownload()
        {
            var config = GetConfig();
            config.PatentSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void UpdatePatentLastUpload()
        {
            var config = GetConfig();
            config.PatentSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void UpdateDesignLastDownload()
        {
            var config = GetConfig();
            config.DesignSettings.ScrapTimings.LastDownload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
        public static void UpdateDesignLastUpload()
        {
            var config = GetConfig();
            config.DesignSettings.ScrapTimings.LastUpload = DateTime.Now;
            using var streamWriter = new StreamWriter(RuntimeConfigs.ApplicationStartupPath + "\\liveconfigs.json");
            var serializer = new JsonSerializer();
            serializer.Serialize(streamWriter, config);
        }
    }
    #endregion

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
}