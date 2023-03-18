using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.DataSaver.Concrete;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;
using TPHunter.Source.Scrapper.Enums;
using TPHunter.Source.Scrapper.Services.Main;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Main.Workers
{
    public class WorkerTasks
    {
        private readonly ITurkPatentClientService _turkPatentClientService;
        private readonly IScrapperClientHelperService _scrapperClientHelperService;
        public WorkerTasks()
        {
            _turkPatentClientService = Ioc.Resolve<ITurkPatentClientService>(nameof(TurkPatentClientService));
            _scrapperClientHelperService = DataSaver.DI.Ioc.Resolve<IScrapperClientHelperService>(nameof(ScrapperClientHelperService));

        }
        public Task DownloadTrademark()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetTrademarkParam();
                var config = LiveConfigFunctions.GetConfig();
              
                do
                {
                    var workers = new Dictionary<int, Task>();



                    var currentBulletin = config.TrademarkSettings.CurrentBulletin+1;


                    for (var i = currentBulletin;
                         (i < currentBulletin + RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount) &&
                         (i <= currentRemoteBulletin.BulletinNumber);
                         i++)
                    {
                        var cacheNumber = i;
                        workers.Add(
                            cacheNumber,
                            Task.Run(() => new Worker<MarkModel>(WorkType.Trademark).Download(new SearchParam()
                            {
                                BulletinNumber = cacheNumber
                            }))
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    LiveConfigFunctions.SetTradeMarkCurrentBulletin(workers.Keys.Max());
                    config = LiveConfigFunctions.GetConfig();
                } while (config.TrademarkSettings.CurrentBulletin <
                         currentRemoteBulletin.BulletinNumber);


                return Task.CompletedTask;
            });

        }
        public Task DownloadDesign()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetDesignParam();
                var config = LiveConfigFunctions.GetConfig();
             
                do
                {
                    var workers = new Dictionary<int, Task>();



                    var currentBulletin = config.DesignSettings.CurrentBulletin+1;


                    for (var i = currentBulletin;
                         (i < currentBulletin + RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount) &&
                         (i <= currentRemoteBulletin.BulletinNumber);
                         i++)
                    {
                        var cacheNumber = i;
                        workers.Add(
                            cacheNumber,
                            Task.Run(() =>
                            {
                                Task.Run(() => new Worker<DesignModel>(WorkType.Design).Download(new SearchParam()
                                {
                                    BulletinNumber = cacheNumber
                                }));
                            })
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    LiveConfigFunctions.SetDesignCurrentBulletin(workers.Keys.Max());
                    config = LiveConfigFunctions.GetConfig();
                } while (config.DesignSettings.CurrentBulletin <
                         currentRemoteBulletin.BulletinNumber);


                return Task.CompletedTask;
            });

        }
        public Task DownloadPatent()
        {
            return Task.Run(async () =>
            {
                var currentRemoteBulletin = await _turkPatentClientService.GetPatentParam();
                var config = LiveConfigFunctions.GetConfig();
           
                do
                {
                    var workers = new Dictionary<DateTime, Task>();



                    var currentBulletin = config.PatentSettings.CurrentPulledDate.AddMonths(1);


                    for (var i = currentBulletin;
                         (i < currentBulletin.AddMonths(RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)) &&
                         (i <= currentRemoteBulletin.StartDate);
                         i = i.AddMonths(1))
                    {
                        var cacheDate = i;
                        workers.Add(
                            cacheDate,
                            Task.Run(() => new Worker<PatentModel>(WorkType.Patent).Download(new SearchParam()
                            {
                                StartDate = cacheDate,
                                EndDate = new DateTime(cacheDate.Year, cacheDate.Month,
                                    cacheDate.Year == DateTime.Now.Year && cacheDate.Month == DateTime.Now.Month
                                        ? DateTime.Now.Day
                                        : DateTime.DaysInMonth(cacheDate.Year, cacheDate.Month))
                            }))
                        );
                    }

                    Task.WaitAll(workers.Values.ToArray());
                    LiveConfigFunctions.SetPatentCurrentBulletin(workers.Keys.Max());
                    config = LiveConfigFunctions.GetConfig();
                } while (config.PatentSettings.CurrentPulledDate <
                         currentRemoteBulletin.StartDate);


                return Task.CompletedTask;
            });

        }

        public Task UploadTrademark()
        {
            return Task.Run(async () =>
            {
                var config = LiveConfigFunctions.GetConfig();
                if (!config.TrademarkSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.TrademarkSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
           
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
                            new Worker<MarkModel>(WorkType.Trademark).Update(applicationNumbers.Skip(uploadPerBrowser * cache)
                                .Take(uploadPerBrowser).ToArray()));

                    }

                    Task.WaitAll();
                    return Task.CompletedTask;
                }

                for (var k = 0;
                     k < applicationNumbers.Count;
                     k += RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser *
                          RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount)
                {
                    var kCache = k;
                    for (var i = 0; i < RuntimeConfigs.GeneralConfig.MainConfig.BrowserCount; i++)
                    {
                        var iCache = i;
                        workers[i] = Task.Run(() =>
                            new Worker<MarkModel>(WorkType.Trademark).Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray()));
                       
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
                var config = LiveConfigFunctions.GetConfig();
                if (!config.PatentSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.PatentSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
        
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
                            new Worker<PatentModel>(WorkType.Patent).Update(applicationNumbers.Skip(uploadPerBrowser * cache)
                                .Take(uploadPerBrowser).ToArray()));
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
                            new Worker<PatentModel>(WorkType.Patent).Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray()));
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
                var config = LiveConfigFunctions.GetConfig();
                if (!config.DesignSettings.UpdateConditions.UpdateAttorneyNames.Any() && !config.DesignSettings.UpdateConditions.UpdateHolderCodes.Any())
                {
                    return Task.CompletedTask;
                }
             
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
                        new Worker<DesignModel>(WorkType.Design).Update(applicationNumbers.Skip(uploadPerBrowser * cache).Take(uploadPerBrowser).ToArray()));
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
                            new Worker<DesignModel>(WorkType.Design).Update(applicationNumbers
                                .Skip((RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser * iCache) + kCache)
                                .Take(RuntimeConfigs.GeneralConfig.MainConfig.MaxUploadPerBrowser).ToArray()));
                    }
                    Task.WaitAll();
                }
                return Task.CompletedTask;

            });
        }
    }
}
