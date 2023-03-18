using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Helpers;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Functions;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class DesignPage : IPage<DesignModel>,IDisposable
    {
        private IWebDriver _webDriver;
        public void SetDriver(IWebDriver driver)
        {
            _webDriver = driver;
        }
        public bool CheckAndClickNext()
        {
            return _webDriver.CheckPageIsLastAndClick();
        }
        public int GetDataCount()
        {
            return _webDriver.GetDataCount();
        }
        public void Prepare()
        {
            _webDriver.GoTpPage();
            _webDriver.ClickSearchType(MainHelper.SearchType.Design);
        }

        public IEnumerable<DesignModel> ScrapMulti()
        {
            var responseListTableModels = _webDriver.GetResponseListTableRows().GetResponseListButtons();
            List<DesignModel> designModels = new();
            foreach (var responseListTableModel in responseListTableModels)
            {
                _webDriver.ClickWithJs(responseListTableModel.DetailButton);
                var data = _webDriver.GetDesignData();
                if (data is null)
                {
                    var applicationNumber =
                        _webDriver.FindElements(By.ClassName("MuiTableRow-hover"), 20)[designModels.Count]
                            .FindElements(By.TagName("td"))[1].Text;
                    designModels.Add(new DesignModel()
                    {
                        ApplicationNumber = applicationNumber
                    });
                }
                else
                {
                    designModels.Add(data);
                    _webDriver.CloseDataPopUp();
                }

            
            }
            return designModels;
        }

        public DesignModel ScrapSingle()
        {
            return _webDriver.GetDesignData();
        }

        public void Search(ISearchParam searchParam)
        {
            _webDriver.SearchDesigns(searchParam.BulletinNumber.ToString());
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }

        public void Dispose()
        {
            _webDriver?.Dispose();
        }
    }
}
