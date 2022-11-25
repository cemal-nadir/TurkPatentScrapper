using System.Collections.Generic;
using System.Threading.Tasks;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Base;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class DesignWorker : IWorker
    {
        public DesignWorker()
        {
            Browser.DI.Ioc.ChromeBaseFactory();
            var driver = Browser.DI.Ioc.Resolve<IBrowserBase>().GenerateBrowser();
            Ioc.DesignPageFactory(driver);
         
        }
        public Task Download()
        {
            return Task.Run(() =>
            {
                var ısLastPage = false;
                #region Test Amaçlı
                var number = 200;
                #endregion
                Ioc.Resolve<IPage<DesignModel>>().Prepare();
                Ioc.Resolve<IPage<DesignModel>>().Search(new BulletinParam()
                {
                    BulletinNumber = number
                });
                while (!ısLastPage)
                {
                    var scrappedDatas = Ioc.Resolve<IPage<DesignModel>>().ScrapMulti();
                    foreach (var scrappedData in scrappedDatas)
                    {
                      

                        #region Veritabanına Gönderme İşlemleri
                        
                        #endregion
                    }

                    ısLastPage = Ioc.Resolve<IPage<DesignModel>>().CheckAndClickNext();

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
                Ioc.Resolve<IPage<DesignModel>>().Prepare();

                foreach (var testData in testDatas)
                {
                    Ioc.Resolve<IPage<DesignModel>>().Search(testData);
                    var scrappedData = Ioc.Resolve<IPage<DesignModel>>().ScrapSingle();
                  

                    #region Veritabanına Gönderme İşlemleri

                    #endregion
                }

                return Task.CompletedTask;
            });
        }
    }
}
