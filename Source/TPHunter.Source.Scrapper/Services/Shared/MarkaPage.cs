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
    public class MarkaPage : IPage<MarkModel>,IDisposable
    {
        private IWebDriver _webDriver;
        public void SetDriver(IWebDriver driver)
        {
            _webDriver = driver;
        }

        public int GetDataCount()
        {
            return _webDriver.GetDataCount();
        }

        public bool CheckAndClickNext()
        {
           return _webDriver.CheckPageIsLastAndClick();
        }

        public void Prepare()
        {
            _webDriver.GoTpPage();
            _webDriver.ClickSearchType(MainHelper.SearchType.Trademark);
        }

        public IEnumerable<MarkModel> ScrapMulti()
        {

           var responseListTableModels= _webDriver.GetResponseListTableRows().GetResponseListButtons();
            List<MarkModel> markModels = new();
            foreach(var responseListTableModel in responseListTableModels)
            {
                _webDriver.ClickWithJs(responseListTableModel.DetailButton);
                var data = _webDriver.GetMarkData();
                if (data is null)
                {
                    var applicationNumber =
                        _webDriver.FindElements(By.ClassName("MuiTableRow-hover"), 20)[markModels.Count]
                            .FindElements(By.TagName("td"))[1].Text;
                    markModels.Add(new MarkModel()
                    {
                        ApplicationNumber = applicationNumber
                    });
                }
                else
                {
                    markModels.Add(data);
                    _webDriver.CloseDataPopUp();
                }

   
            }
            return markModels;

        }

        public MarkModel ScrapSingle()
        {
            return _webDriver.GetMarkData();
        }

     

        public void Search(ISearchParam searchParam)
        {
            _webDriver.SearchMarks(searchParam.BulletinNumber.ToString());
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
