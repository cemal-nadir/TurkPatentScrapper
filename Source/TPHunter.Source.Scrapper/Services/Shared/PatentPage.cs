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
    public class PatentPage : IPage<PatentModel>, IDisposable
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
            _webDriver.ClickSearchType(MainHelper.SearchType.Patent);
        }

        public IEnumerable<PatentModel> ScrapMulti()
        {
            var responseListTableModels = _webDriver.GetResponseListTableRows().GetResponseListButtons();
            List<PatentModel> patentModels = new();
            foreach (var responseListTableModel in responseListTableModels)
            {
                _webDriver.ClickWithJs(responseListTableModel.DetailButton);
                var data = _webDriver.GetPatentData();
                if (data is null)
                {
                    var applicationNumber =
                        _webDriver.FindElements(By.ClassName("MuiTableRow-hover"), 20)[patentModels.Count]
                            .FindElements(By.TagName("td"))[1].Text;
                    patentModels.Add(new PatentModel()
                    {
                        ApplicationNumber = applicationNumber
                    });
                }
                else
                {
                    patentModels.Add(data);
                    _webDriver.CloseDataPopUp();
                }

             
            }
            return patentModels;
        }

        public PatentModel ScrapSingle()
        {
            return _webDriver.GetPatentData();
        }


        public void Search(ISearchParam searchParam)
        {
            _webDriver.SearchPatents(searchParam.StartDate ?? DateTime.Now, searchParam.EndDate?? DateTime.Now);
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
