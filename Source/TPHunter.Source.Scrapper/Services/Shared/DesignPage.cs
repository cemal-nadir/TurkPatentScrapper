using OpenQA.Selenium;
using System.Collections.Generic;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Helpers;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Functions;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class DesignPage : IPage<DesignModel>
    {
        private readonly IWebDriver _webDriver;
        public DesignPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
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
                designModels.Add(_webDriver.GetDesignData(MainHelper.ScrapType.Download));
                _webDriver.CloseDataPopUp();
            }
            return designModels;
        }

        public DesignModel ScrapSingle()
        {
            return _webDriver.GetDesignData(MainHelper.ScrapType.Upload);
        }

        public void Search(ISearchParam searchParam)
        {
            _webDriver.SearchDesigns(((BulletinParam)searchParam).BulletinNumber.ToString());
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }
    }
}
