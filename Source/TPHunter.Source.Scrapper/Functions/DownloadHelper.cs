using OpenQA.Selenium;
using System;
using System.Linq;
using TPHunter.Source.Browser.Helpers;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class DownloadHelper
    {
        public static void SearchMarks(this IWebDriver driver, string bulletinNumber)
        {
            var tabPanelDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss2.jss5 > main > div.jss219 > div > div.jss221 > div.jss227 > div:nth-child(1)"), 20);
            driver.ClickWithJs(tabPanelDiv.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("aria-label") == "Marka Araştırma"));
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).SendKeys(bulletinNumber);
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=__next]/div/div[2]/main/div[1]/div/div[1]/div[2]/div[2]/div/button[2]"), 20));
        }
        public static void SearchDesigns(this IWebDriver driver, string bulletinNumber)
        {
            var tabPanelDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss2.jss5 > main > div.jss219 > div > div.jss221 > div.jss227 > div:nth-child(1)"), 20);
            driver.ClickWithJs(tabPanelDiv.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("aria-label") == "Tasarım Araştırma"));
            driver.FindElement(By.CssSelector("input[placeholder='Bülten Numarası']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Bülten Numarası']"), 20).SendKeys(bulletinNumber);
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=__next]/div/div[2]/main/div[1]/div/div[1]/div[2]/div[2]/div/button[2]"), 20));
        }
        public static void SearchPatents(this IWebDriver driver, DateTime startTime,DateTime endTime)
        {
            var tabPanelDiv = driver.FindElement(By.CssSelector("#__next > div > div.jss2.jss5 > main > div.jss219 > div > div.jss221 > div.jss227 > div:nth-child(1)"), 20);
            driver.ClickWithJs(tabPanelDiv.FindElements(By.TagName("button")).FirstOrDefault(x => x.GetAttribute("aria-label") == "Patent Araştırma"));
            driver.FindElement(By.CssSelector("input[placeholder='Yayın Tarihi']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Yayın Tarihi']"), 20).SendKeys(startTime.ToString("dd/MM/yyyy"));
            driver.FindElement(By.CssSelector("input[placeholder='Yayın Bitiş Tarihi']"), 20).Click();
            driver.FindElement(By.CssSelector("input[placeholder='Yayın Bitiş Tarihi']"), 20).SendKeys(endTime.ToString("dd/MM/yyyy"));
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=__next]/div/div[2]/main/div[1]/div/div[1]/div[2]/div[2]/div/button[2]"), 20));
        }
     
    }
}
