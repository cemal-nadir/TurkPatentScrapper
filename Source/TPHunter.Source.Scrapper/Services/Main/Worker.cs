using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Browser.Base;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Enums;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class Worker<TModel>:IWorker where TModel: class, IModel
    {
        private readonly IScrapperClientService<TModel> _scrapperClientService;
        private readonly IBrowserBase _browserBase;
        private readonly IPage<TModel> _pageService;
        private Guid _browserId;

        public Worker(WorkType workType)
        {
            _browserBase = Browser.DI.Ioc.Resolve<IBrowserBase>(nameof(ChromeBase));

            switch (workType)
            {
                case WorkType.Trademark:
                    _pageService = DI.Ioc.Resolve<IPage<TModel>>(nameof(MarkaPage));
                    _scrapperClientService =
                        DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServiceTrademark");
                 
                    break;
                case WorkType.Design:
                    _pageService = DI.Ioc.Resolve<IPage<TModel>>(nameof(DesignPage));
                    _scrapperClientService =
                        DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServiceDesign");
                    break;
                case WorkType.Patent:
                    _pageService = DI.Ioc.Resolve<IPage<TModel>>(nameof(PatentPage));
                    _scrapperClientService =
                        DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServicePatent");
                    break;
                default:
                    _pageService = null;
                    _scrapperClientService = null;
                    break;
            }
        }

        public async Task Download(ISearchParam searchParam)
        {
            while (true)
            {
                try
                {
                    _browserBase.DisposeBrowser(_browserId);
                    _browserId = Guid.NewGuid();
                    _pageService.SetDriver(_browserBase.Browser(_browserId));
                    _pageService.Prepare();
                    _pageService.Search(searchParam);
                    var ısLastPage = false;
                    var remoteDataCount = _pageService.GetDataCount();
                    var localDatas = _scrapperClientService.GetLastPulledApplicationNumbersAsync(searchParam).Result.Data;

                    var enumerable = localDatas is null ? new List<string>() : localDatas.ToList();
                    var difference = remoteDataCount - enumerable.Count();

                    while (!ısLastPage && difference > 0)
                    {
                        var scrappedDatas = _pageService.ScrapMulti();

                        foreach (var scrappedData in scrappedDatas)
                        {
                            if (enumerable.Any(x => x == scrappedData.ApplicationNumber)) continue;
                            scrappedData.Bulletin = searchParam.StartDate is { }
                                ? searchParam.StartDate.Value.ToShortDateString()
                                : searchParam.BulletinNumber.ToString();
                            _scrapperClientService.InsertAsync(scrappedData).GetAwaiter().GetResult();

                            difference--;
                        }

                        ısLastPage = _pageService.CheckAndClickNext();
                        if (!ısLastPage || difference <= 0) continue;
                        var removeDatas = _scrapperClientService.GetLastPulledIdsAsync(searchParam).Result.Data;

                        foreach (var removeData in removeDatas)
                        {
                            _scrapperClientService.RemoveAsync(removeData).GetAwaiter().GetResult();
                        }

                        await Download(searchParam);

                    }

                    _browserBase.DisposeBrowser(_browserId);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
          
          
        }





        public async Task Update(string[] applicationNumbers)
        {
            _browserId = Guid.NewGuid();
            _pageService.SetDriver(_browserBase.Browser(_browserId));
            _pageService.Prepare();
            foreach (var applicationNumber in applicationNumbers)
            {
                _pageService.Search(applicationNumber);
                var scrappedData = _pageService.ScrapSingle();

               await _scrapperClientService.UpdateAsync(scrappedData);
            }
            _browserBase.DisposeBrowser(_browserId);
        }

    }
}
