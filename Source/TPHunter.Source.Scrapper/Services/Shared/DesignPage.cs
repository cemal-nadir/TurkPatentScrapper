using Browser.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Models.Scrapper;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Functions;
using TPHunter.Source.Scrapper.Models;

namespace TPHunter.Source.Scrapper.Services.Shared
{
    public class DesignPage<T> : IPage<DesignModel>
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

        public void Prepare()
        {
            _webDriver.GoTPPage();
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

        public void Search(object searchParam)
        {
            _webDriver.SearchDesigns(searchParam.ToString());
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }
    }
}
