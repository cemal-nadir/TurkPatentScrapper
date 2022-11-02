using Browser.Helpers;
using OpenQA.Selenium;
using System.Collections.Generic;
using TPHunter.Source.Core.Models.Scrapper;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Functions;
using TPHunter.Source.Scrapper.Models;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class MarkaPage<T> : IPage<MarkModel>
    {
        private readonly IWebDriver _webDriver;
        public MarkaPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool CheckAndClickNext()
        {
           return _webDriver.CheckPageIsLastAndClick();
        }

        public void Prepare()
        {
            _webDriver.GoTPPage();
            _webDriver.ClickSearchType(MainHelper.SearchType.Trademark);
        }

        public IEnumerable<MarkModel> ScrapMulti()
        {

           var responseListTableModels= _webDriver.GetResponseListTableRows().GetResponseListButtons();
            List<MarkModel> markModels = new();
            foreach(var responseListTableModel in responseListTableModels)
            {
                _webDriver.ClickWithJs(responseListTableModel.DetailButton);
                markModels.Add(_webDriver.GetMarkData(MainHelper.ScrapType.Download));
                _webDriver.CloseDataPopUp();
            }
            return markModels;

        }

        public MarkModel ScrapSingle()
        {
            return _webDriver.GetMarkData(MainHelper.ScrapType.Upload);
        }

        public void Search(object searchParam)
        {
            _webDriver.SearchMarks(searchParam.ToString());
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }
    }
}
