using OpenQA.Selenium;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Helpers;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Functions;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class MarkaPage : IPage<MarkModel>
    {
        private readonly IWebDriver _webDriver;
        public MarkaPage(IWebDriver webDriver)
        {

            _webDriver = webDriver;

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
                markModels.Add(_webDriver.GetMarkData(MainHelper.ScrapType.Download));
                _webDriver.CloseDataPopUp();
            }
            return markModels;

        }

        public MarkModel ScrapSingle()
        {
            return _webDriver.GetMarkData(MainHelper.ScrapType.Upload);
        }

     

        public void Search(ISearchParam searchParam)
        {
            _webDriver.SearchMarks(((BulletinParam)searchParam).BulletinNumber.ToString());
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }
    }
}
