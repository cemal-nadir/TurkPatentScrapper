using Browser.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.Scrapper.Models;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class MainHelper
    {
        public static void GoTPPage(this IWebDriver driver)
        {
            driver.Navigate().GoToUrl(RuntimeConfigs.TpSearchPage);
        }
        public static void ClickSearchType(this IWebDriver driver, SearchType searchType)
        {
            var searchDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss22.jss25 > main > div.jss1 > div > div.jss3 > div.MuiTabs-root.jss13.MuiTabs-vertical > div.MuiTabs-scroller.MuiTabs-scrollable > div"), 5);
            var buttons = searchDiv.FindElements(By.TagName("button"));
            switch (searchType)
            {
                case SearchType.Trademark:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Marka Araştırma"));
                    break;
                case SearchType.Patent:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Patent Araştırma"));
                    break;
                case SearchType.Design:
                    driver.ClickWithJs(buttons.FirstOrDefault(x => x.GetAttribute("title") == "Tasarım Araştırma"));
                    break;
            }
        }
        public static void CloseDataPopUp(this IWebDriver driver)
        {
            var divs=driver.FindElements(By.ClassName("react-draggable"),20);
            var button = (from div in divs
                          let b = div.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("title") == "Kapat")
                          where b != null
                          select b).FirstOrDefault();
                driver.ClickWithJs(button);
        }
        public static IEnumerable<IWebElement> GetResponseListTableRows(this IWebDriver driver)
        {
            return driver.FindElement(By.CssSelector("#results > tbody"), 20).FindElements(By.TagName("tr"));
        }
        public static IEnumerable<ResponseListTableModel> GetResponseListButtons(this IEnumerable<IWebElement> webElements)
        {
            List<ResponseListTableModel> responseListTableModels = new();
            foreach (var rows in webElements)
            {
                var cols = rows.FindElements(By.TagName("td"));
                var button = cols.LastOrDefault().FindElement(By.TagName("button"));
                var applicationNumber = cols.FirstOrDefault(x => x.GetAttribute("role") == "applicationNumber");
                responseListTableModels.Add(new()
                {
                    ApplicationNumber = applicationNumber.Text,
                    DetailButton = button
                });
            }
            return responseListTableModels;
        }






        public enum SearchType
        {
            Trademark,
            Patent,
            Design
        }
    }
}
