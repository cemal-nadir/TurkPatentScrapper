using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;

namespace TPHunter.Source.Main.Workers
{
    public class WorkerTasks
    {
        private readonly ITurkPatentClientService _turkPatentClientService;
        private readonly IScrapperClientHelperService _scrapperClientHelperService;
        private readonly LiveConfig _liveConfig;
        public WorkerTasks(LiveConfig liveConfig)
        {
            Shared.Scrapper.Ioc.TurkPatentClientFactory();
            DataSaver.DI.Ioc.ScrapperClientHelperFactory();
            Ioc.TurkPatentClientServiceFactory();
            _turkPatentClientService = Ioc.Resolve<ITurkPatentClientService>();
            _scrapperClientHelperService = DataSaver.DI.Ioc.Resolve<IScrapperClientHelperService>();
            _liveConfig = liveConfig;
        }
        public Task DownloadTrademark()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetTrademarkParam();
                var config = _liveConfig.GetConfig();
                Ioc.WorkerFactory<MarkModel>();
                do
                {
                    var workers = new Dictionary<int, Task>();



                    var currentBulletin = config.TrademarkSettings.CurrentBulletin;


                    for (var i = currentBulletin;
                         (i < currentBulletin + RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount) &&
                         (i <= ((BulletinParam)currentRemoteBulletin).BulletinNumber);
                         i++)
                    {
                        var cacheNumber = i;
                        workers.Add(
                            cacheNumber,
                            Task.Run(() =>
                            {
                                Ioc.Resolve<IWorker>().Download(new BulletinParam()
                                {
                                    BulletinNumber = cacheNumber
                                });
                            })
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    _liveConfig.SetTradeMarkCurrentBulletin(workers.Keys.Max());
                    config = _liveConfig.GetConfig();
                } while (config.TrademarkSettings.CurrentBulletin <
                         ((BulletinParam)currentRemoteBulletin).BulletinNumber);


                return Task.CompletedTask;
            });

        }
        public Task DownloadDesign()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetDesignParam();
                var config = _liveConfig.GetConfig();
                Ioc.WorkerFactory<DesignModel>();
                do
                {
                    var workers = new Dictionary<int, Task>();



                    var currentBulletin = config.DesignSettings.CurrentBulletin;


                    for (var i = currentBulletin;
                         (i < currentBulletin + RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount) &&
                         (i <= ((BulletinParam)currentRemoteBulletin).BulletinNumber);
                         i++)
                    {
                        var cacheNumber = i;
                        workers.Add(
                            cacheNumber,
                            Task.Run(() =>
                            {
                                Ioc.Resolve<IWorker>().Download(new BulletinParam()
                                {
                                    BulletinNumber = cacheNumber
                                });
                            })
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    _liveConfig.SetDesignCurrentBulletin(workers.Keys.Max());
                    config = _liveConfig.GetConfig();
                } while (config.DesignSettings.CurrentBulletin <
                         ((BulletinParam)currentRemoteBulletin).BulletinNumber);


                return Task.CompletedTask;
            });

        }
        public Task DownloadPatent()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetPatentParam();
                var config = _liveConfig.GetConfig();
                Ioc.WorkerFactory<PatentModel>();
                do
                {
                    var workers = new Dictionary<DateTime, Task>();



                    var currentBulletin = config.PatentSettings.CurrentPulledDate;


                    for (var i = currentBulletin;
                         (i < currentBulletin.AddMonths(RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)) &&
                         (i <= ((DateRangeParam)currentRemoteBulletin).StartDate);
                         i = i.AddMonths(1))
                    {
                        var cacheDate = i;
                        workers.Add(
                            cacheDate,
                            Task.Run(() =>
                            {
                                Ioc.Resolve<IWorker>().Download(new DateRangeParam()
                                {
                                    StartDate = cacheDate,
                                    EndDate = new DateTime(cacheDate.Year, cacheDate.Month,
                                        cacheDate.Year == DateTime.Now.Year && cacheDate.Month == DateTime.Now.Month
                                            ? DateTime.Now.Day
                                            : DateTime.DaysInMonth(cacheDate.Year, cacheDate.Month))
                                });
                            })
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    _liveConfig.SetPatentCurrentBulletin(workers.Keys.Max());
                    config = _liveConfig.GetConfig();
                } while (config.PatentSettings.CurrentPulledDate <
                         ((DateRangeParam)currentRemoteBulletin).StartDate);


                return Task.CompletedTask;
            });

        }

        public Task UploadTrademark()
        {
            return Task.Run(async () =>
            {
                var config = _liveConfig.GetConfig();
                if (!config.TrademarkSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.TrademarkSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
                Ioc.WorkerFactory<MarkModel>();
                List<Guid> attorneyIds = null;
                if (config.TrademarkSettings.UpdateConditions.UpdateAttorneyNames.Any())
                    attorneyIds = (await _scrapperClientHelperService.GetAttorneyIdsByNames(config.TrademarkSettings.UpdateConditions
                        .UpdateAttorneyNames.ToArray())).ToList();

                var applicationNumbers = (await _scrapperClientHelperService.GetFilteredTrademarkApplicationNumbers(
                    attorneyIds != null && attorneyIds.Any() ? attorneyIds.ToArray() : null,
                    config.TrademarkSettings.UpdateConditions.UpdateHolderCodes != null &&
                    config.TrademarkSettings.UpdateConditions.UpdateHolderCodes.Any()
                        ? config.TrademarkSettings.UpdateConditions.UpdateHolderCodes.ToArray()
                        : null)).ToList();

                var workers = new Task[RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount];

                if (applicationNumbers.Count <= RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)
                {
                    var uploadPerBrowser = applicationNumbers.Count /
                                           RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount;
                    uploadPerBrowser = uploadPerBrowser == 0 ? 1 : uploadPerBrowser;

                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var cache = i;
                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers.Skip(uploadPerBrowser * cache).Take(uploadPerBrowser).ToArray());
                        });
                    }

                    Task.WaitAll();
                    return Task.CompletedTask;
                }

                for (var k = 0;
                     k < applicationNumbers.Count;
                     k = k + (RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser *
                              RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount))
                {
                    var kCache = k;
                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var iCache = i;

                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray());
                        });
                    }
                    Task.WaitAll();
                }
                return Task.CompletedTask;

            });
        }

        public Task UploadPatent()
        {
            return Task.Run(async () =>
            {
                var config = _liveConfig.GetConfig();
                if (!config.PatentSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.PatentSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
                Ioc.WorkerFactory<PatentModel>();
                List<Guid> attorneyIds = null;
                if (config.PatentSettings.UpdateConditions.UpdateAttorneyNames.Any())
                    attorneyIds = (await _scrapperClientHelperService.GetAttorneyIdsByNames(config.TrademarkSettings.UpdateConditions
                        .UpdateAttorneyNames.ToArray())).ToList();

                var applicationNumbers = (await _scrapperClientHelperService.GetFilteredPatentApplicationNumbers(
                    attorneyIds != null && attorneyIds.Any() ? attorneyIds.ToArray() : null,
                    config.PatentSettings.UpdateConditions.UpdateHolderCodes != null &&
                    config.PatentSettings.UpdateConditions.UpdateHolderCodes.Any()
                        ? config.PatentSettings.UpdateConditions.UpdateHolderCodes.ToArray()
                        : null)).ToList();

                var workers = new Task[RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount];

                if (applicationNumbers.Count <= RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)
                {
                    var uploadPerBrowser = applicationNumbers.Count /
                                           RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount;
                    uploadPerBrowser = uploadPerBrowser == 0 ? 1 : uploadPerBrowser;

                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var cache = i;
                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers.Skip(uploadPerBrowser * cache).Take(uploadPerBrowser).ToArray());
                        });
                    }

                    Task.WaitAll();
                    return Task.CompletedTask;
                }

                for (var k = 0;
                     k < applicationNumbers.Count;
                     k = k + (RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser *
                              RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount))
                {
                    var kCache = k;
                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var iCache = i;

                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray());
                        });
                    }
                    Task.WaitAll();
                }
                return Task.CompletedTask;

            });
        }

        public Task UploadDesign()
        {
            return Task.Run(async () =>
            {
                var config = _liveConfig.GetConfig();
                if (!config.DesignSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.DesignSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
                Ioc.WorkerFactory<DesignModel>();
                List<Guid> attorneyIds = null;
                if (config.DesignSettings.UpdateConditions.UpdateAttorneyNames.Any())
                    attorneyIds = (await _scrapperClientHelperService.GetAttorneyIdsByNames(config.DesignSettings.UpdateConditions
                        .UpdateAttorneyNames.ToArray())).ToList();

                var applicationNumbers = (await _scrapperClientHelperService.GetFilteredPatentApplicationNumbers(
                    attorneyIds != null && attorneyIds.Any() ? attorneyIds.ToArray() : null,
                    config.DesignSettings.UpdateConditions.UpdateHolderCodes != null &&
                    config.DesignSettings.UpdateConditions.UpdateHolderCodes.Any()
                        ? config.DesignSettings.UpdateConditions.UpdateHolderCodes.ToArray()
                        : null)).ToList();

                var workers = new Task[RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount];

                if (applicationNumbers.Count <= RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)
                {
                    var uploadPerBrowser = applicationNumbers.Count /
                                           RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount;
                    uploadPerBrowser = uploadPerBrowser == 0 ? 1 : uploadPerBrowser;

                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var cache = i;
                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers.Skip(uploadPerBrowser * cache).Take(uploadPerBrowser).ToArray());
                        });
                    }

                    Task.WaitAll();
                    return Task.CompletedTask;
                }

                for (var k = 0;
                     k < applicationNumbers.Count;
                     k = k + (RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser *
                              RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount))
                {
                    var kCache = k;
                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var iCache = i;

                        workers[i] = Task.Run(() =>
                        {
                            Ioc.Resolve<IWorker>().Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray());
                        });
                    }
                    Task.WaitAll();
                }
                return Task.CompletedTask;

            });
        }
    }
}
