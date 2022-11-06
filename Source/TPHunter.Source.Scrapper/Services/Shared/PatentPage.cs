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

namespace TPHunter.Source.Scrapper.Services.Shared
{
   public class PatentPage<T>:IPage<PatentModel>
    {
        private readonly IWebDriver _webDriver;
        public PatentPage(IWebDriver webDriver)
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
            _webDriver.ClickSearchType(MainHelper.SearchType.Patent);
        }

        public IEnumerable<PatentModel> ScrapMulti()
        {
            var responseListTableModels = _webDriver.GetResponseListTableRows().GetResponseListButtons();
            List<PatentModel> patentModels = new();
            foreach (var responseListTableModel in responseListTableModels)
            {
                _webDriver.ClickWithJs(responseListTableModel.DetailButton);
                patentModels.Add(_webDriver.GetPatentData(MainHelper.ScrapType.Download));
                _webDriver.CloseDataPopUp();
            }
            return patentModels;
        }

        public PatentModel ScrapSingle()
        {
            return _webDriver.GetPatentData(MainHelper.ScrapType.Upload);
        }

        public void Search(object searchParam)
        {
            var dates = searchParam as DateTime[];
            _webDriver.SearchPatents(dates[0],dates[1]);
        }

        public void Search(string applicationNumber)
        {
            _webDriver.SearchSingle(applicationNumber);
        }
    }
}
