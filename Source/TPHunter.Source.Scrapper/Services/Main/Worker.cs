using System.Linq;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Source.Browser.Base;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class Worker : IWorker
    {
        private readonly IScrapperClientService<IModel> _scrapperClientService;
        private readonly IPage<IModel> _pageService;
        private readonly IBrowserBase _browserBase;
        public Worker(IScrapperClientService<IModel> scrapperClientService,IPage<IModel>pageService,IBrowserBase browserBase)
        {

            _scrapperClientService = scrapperClientService;
            _browserBase = browserBase;
            _pageService = pageService;


        }
        public async Task Download(ISearchParam searchParam)
        {
            var ısLastPage = false;

            _pageService.Prepare();
            _pageService.Search(searchParam);

            var remoteDataCount = _pageService.GetDataCount();
            var localDatas = (await _scrapperClientService.GetLastPulledApplicationNumbersAsync(searchParam)).ToList();
            var difference = remoteDataCount - localDatas.Count;

            while (!ısLastPage || difference > 0)
            {
                var scrappedDatas = _pageService.ScrapMulti();
                foreach (var scrappedData in scrappedDatas)
                {
                    if (localDatas.Any(x => x == scrappedData.ApplicationNumber)) continue;
                    await _scrapperClientService.InsertAsync(scrappedData);
                    difference--;
                }

                ısLastPage = _pageService.CheckAndClickNext();
                if (!ısLastPage || difference <= 0) continue;
                var removeDatas = await _scrapperClientService.GetLastPulledIdsAsync(searchParam);
                foreach (var removeData in removeDatas)
                {
                    await _scrapperClientService.RemoveAsync(removeData);
                }

                await Download(searchParam);

            }
            _browserBase.DisposeBrowser();
        }

        public async Task Update(string[] applicationNumbers)
        {
            _pageService.Prepare();
            foreach (var applicationNumber in applicationNumbers)
            {
                _pageService.Search(applicationNumber);
                var scrappedData = _pageService.ScrapSingle();

                await _scrapperClientService.UpdateAsync(scrappedData);
            }
            _browserBase.DisposeBrowser();
        }

    }
}
