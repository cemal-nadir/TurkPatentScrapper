using Browser.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;
using TPHunter.Source.Scrapper.Models;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class MarkaWorker : IWorker
    {
        protected readonly IWebDriver _driver;

        public MarkaWorker()
        {
            Browser.DI.Ioc.ChromeBaseFactory();
            _driver = Browser.DI.Ioc.Resolve<IBrowserBase>().GenerateBrowser();
            Ioc.MarkaPageFactory(_driver);
        }
        public Task Download()
        {
            return Task.Run(() =>
            {
                Ioc.Resolve<IPage<MarkModel>>().Prepare();
                //şimdilik test amaçlı
                int number = 1;

                Ioc.Resolve<IPage<MarkModel>>().Search(number);

                var scrappedDatas=Ioc.Resolve<IPage<MarkModel>>().ScrapMulti();

                #region Resmi Kaydetme İşlemleri
                #endregion

                #region Veritabanına Gönderme İşlemleri
                #endregion

            });




        }

        public Task Update()
        {
            throw new NotImplementedException();
        }
    }
}
