using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Base;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class PatentWorker : IWorker
    {
        public PatentWorker()
        {
            Browser.DI.Ioc.ChromeBaseFactory();
            var driver = Browser.DI.Ioc.Resolve<IBrowserBase>().GenerateBrowser();
            Ioc.PatentPageFactory(driver);
        }
        public Task Download()
        {
            return Task.Run(() =>
            {
                var ısLastPage = false;
                #region Test Amaçlı
                var dates = new DateTime[] {
                    DateTime.Now.AddDays(-30),
                    DateTime.Now
                };
                #endregion
                Ioc.Resolve<IPage<PatentModel>>().Prepare();
                Ioc.Resolve<IPage<PatentModel>>().Search(dates);
                while (!ısLastPage)
                {
                    var scrappedDatas = Ioc.Resolve<IPage<PatentModel>>().ScrapMulti();
                    foreach (var scrappedData in scrappedDatas)
                    {

                        #region Veritabanına Gönderme İşlemleri

                        #endregion
                    }

                    ısLastPage = Ioc.Resolve<IPage<PatentModel>>().CheckAndClickNext();

                }

                return Task.CompletedTask;
            });
        }

        public Task Update()
        {
            return Task.Run(() => {

                #region Test Amaçlı
                var testDatas = new List<string>() { "2012/12230", "2012/12231" };
                #endregion
                Ioc.Resolve<IPage<PatentModel>>().Prepare();

                foreach (var testData in testDatas)
                {
                    Ioc.Resolve<IPage<PatentModel>>().Search(testData);
                    var scrappedData = Ioc.Resolve<IPage<PatentModel>>().ScrapSingle();
                 

                    #region Veritabanına Gönderme İşlemleri

                    #endregion
                }

                return Task.CompletedTask;
            });
        }
    }
}
