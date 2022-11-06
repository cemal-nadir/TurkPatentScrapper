using Browser.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.Core.Models.Scrapper;
using TPHunter.Source.FileStorage;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.DI;

namespace TPHunter.Source.Scrapper.Services.Main
{
    public class PatentWorker : IWorker
    {
        private readonly IWebDriver _driver;
        public PatentWorker()
        {
            Browser.DI.Ioc.ChromeBaseFactory();
            _driver = Browser.DI.Ioc.Resolve<IBrowserBase>().GenerateBrowser();
            Ioc.PatentPageFactory(_driver);
            FileStorage.DI.Ioc.AmazonStorageFactory();
            FileStorage.DI.Ioc.Resolve<IFileTransferManager<AmazonS3Config>>().FileStorageConfig = RuntimeConfigs.GeneralConfig.AmazonConfig.AmazonS3Config;
        }
        public Task Download()
        {
            return Task.Run(async () =>
            {
                bool IsLastPage = false;
                #region Test Amaçlı
                DateTime[] dates = new DateTime[] {
                    DateTime.Now.AddDays(-30),
                    DateTime.Now
                };
                #endregion
                Ioc.Resolve<IPage<PatentModel>>().Prepare();
                Ioc.Resolve<IPage<PatentModel>>().Search(dates);
                while (!IsLastPage)
                {
                    var scrappedDatas = Ioc.Resolve<IPage<PatentModel>>().ScrapMulti();
                    foreach (var scrappedData in scrappedDatas)
                    {
                        #region Resmi Kaydetme İşlemleri
                        //         var fileId = await FileStorage.DI.Ioc.Resolve<IFileTransferManager<AmazonS3Config>>().Upload(scrappedData.ImageText.FixMarkImageBase64(), "jpg", SearchType.Trademark.ToString());
                        #endregion

                        #region Veritabanına Gönderme İşlemleri

                        #endregion
                    }

                    IsLastPage = Ioc.Resolve<IPage<PatentModel>>().CheckAndClickNext();

                }







            });
        }

        public Task Update()
        {
            return Task.Run(async () => {

                #region Test Amaçlı
                List<string> testDatas = new List<string>() { "2012/12230", "2012/12231" };
                #endregion
                Ioc.Resolve<IPage<PatentModel>>().Prepare();

                foreach (var testData in testDatas)
                {
                    Ioc.Resolve<IPage<PatentModel>>().Search(testData);
                    var scrappedData = Ioc.Resolve<IPage<PatentModel>>().ScrapSingle();
                    #region Resmi Kaydetme İşlemleri
                    //Önce kaydedilmiş eski resmi siliyorum fileid yerine mevcuttaki resmin fileid'si gelecek
                    //await FileStorage.DI.Ioc.Resolve<IFileTransferManager<AmazonS3Config>>().DeleteFile("fileid", "jpg", SearchType.Trademark.ToString());
                    //var fileId = await FileStorage.DI.Ioc.Resolve<IFileTransferManager<AmazonS3Config>>().Upload(scrappedData.ImageText.FixMarkImageBase64(), "jpg", SearchType.Trademark.ToString());
                    #endregion

                    #region Veritabanına Gönderme İşlemleri

                    #endregion
                }



            });
        }
    }
}
