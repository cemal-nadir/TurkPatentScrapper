using System;
using System.Threading;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.Main.Enums;

namespace TPHunter.Source.Main.Workers
{
    public class MainWorker
    {
        private readonly LiveConfig _liveConfig = new();
        private readonly WorkerTasks _workerTasks;

        public MainWorker()
        {
            _workerTasks = new WorkerTasks(_liveConfig);
        }
        public async Task DoWork()
        {
            while (true)
            {
                await RunWork(GetNextWork());
            }
        }

        private WorkType GetNextWork()
        {
            var config = _liveConfig.GetConfig();

            #region Download Control
            if (config.PatentSettings.ScrapTimings.LastDownload
                    .AddHours(config.PatentSettings.ScrapTimings.DownloadHourDelay)
                    .AddMinutes(config.PatentSettings.ScrapTimings.DownloadMinuteDelay)
                    .AddSeconds(config.PatentSettings.ScrapTimings.DownloadSecondDelay)
                > DateTime.Now
               ) return WorkType.DownloadPatent;
            if (config.DesignSettings.ScrapTimings.LastDownload
                    .AddHours(config.DesignSettings.ScrapTimings.DownloadHourDelay)
                    .AddMinutes(config.DesignSettings.ScrapTimings.DownloadMinuteDelay)
                    .AddSeconds(config.DesignSettings.ScrapTimings.DownloadSecondDelay)
                > DateTime.Now
               ) return WorkType.DownloadDesign;
            if (config.TrademarkSettings.ScrapTimings.LastDownload
                    .AddHours(config.TrademarkSettings.ScrapTimings.DownloadHourDelay)
                    .AddMinutes(config.TrademarkSettings.ScrapTimings.DownloadMinuteDelay)
                    .AddSeconds(config.TrademarkSettings.ScrapTimings.DownloadSecondDelay)
                > DateTime.Now
               ) return WorkType.DownloadTrademark;


            #endregion

            #region Upload Control
            if (config.PatentSettings.ScrapTimings.LastUpload
                    .AddHours(config.PatentSettings.ScrapTimings.UpdateHourDelay)
                    .AddMinutes(config.PatentSettings.ScrapTimings.UpdateMinuteDelay)
                    .AddSeconds(config.PatentSettings.ScrapTimings.UpdateSecondDelay)
                > DateTime.Now
               ) return WorkType.UploadPatent;
            if (config.DesignSettings.ScrapTimings.LastUpload
                    .AddHours(config.DesignSettings.ScrapTimings.UpdateHourDelay)
                    .AddMinutes(config.DesignSettings.ScrapTimings.UpdateMinuteDelay)
                    .AddSeconds(config.DesignSettings.ScrapTimings.UpdateSecondDelay)
                > DateTime.Now
               ) return WorkType.UploadDesign;
            if (config.TrademarkSettings.ScrapTimings.LastUpload
                    .AddHours(config.TrademarkSettings.ScrapTimings.UpdateHourDelay)
                    .AddMinutes(config.TrademarkSettings.ScrapTimings.UpdateMinuteDelay)
                    .AddSeconds(config.TrademarkSettings.ScrapTimings.UpdateSecondDelay)
                > DateTime.Now
               ) return WorkType.UploadTrademark;


            #endregion

            return WorkType.Wait;


        }

        private async Task RunWork(WorkType workType)
        {
            switch (workType)
            {
                case WorkType.DownloadTrademark:

                    await _workerTasks.DownloadTrademark();
                    _liveConfig.UpdateTrademarkLastDownload();
                    break;
                case WorkType.DownloadPatent:
                    await _workerTasks.DownloadPatent();
                    _liveConfig.UpdatePatentLastDownload();
                    break;
                case WorkType.DownloadDesign:
                    await _workerTasks.DownloadDesign();
                    _liveConfig.UpdateDesignLastDownload();
                    break;
                case WorkType.UploadTrademark:
                    await _workerTasks.UploadTrademark();
                    _liveConfig.UpdateTrademarkLastUpload();
                    break;
                case WorkType.UploadPatent:
                    await _workerTasks.UploadPatent();
                    _liveConfig.UpdatePatentLastUpload();
                    break;
                case WorkType.UploadDesign:
                    await _workerTasks.UploadDesign();
                    _liveConfig.UpdateDesignLastUpload();
                    break;
                case WorkType.Wait:
                    Thread.Sleep(TimeSpan.FromSeconds(59));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(workType), workType, null);
            }
        }
    }
}
