using Browser.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class UploadHelper
    {
        public static void SearchSingle(this IWebDriver driver, string applicationNumber)
        {
            var tabPanelDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss2.jss5 > main > div.jss219 > div > div.jss221 > div.jss227 > div:nth-child(1)"), 20);
            driver.ClickWithJs(tabPanelDiv.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("aria-label") == "Dosya Takibi"));
            driver.FindElement(By.CssSelector("input[placeholder='Başvuru Numarası']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Başvuru Numarası']"), 20).SendKeys(applicationNumber);
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=__next]/div/div[2]/main/div[1]/div/div[1]/div[2]/div[2]/div/button[2]"), 20));
        }
    }
}
