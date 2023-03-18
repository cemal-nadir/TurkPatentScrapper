using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;
using TPHunter.Source.Browser.Helpers;

namespace TPHunter.Source.Scrapper.Functions
{
    public static class DownloadHelper
    {
        public static void SearchMarks(this IWebDriver driver, string bulletinNumber)
        {

            driver.ClickWithJs(driver.FindElements(By.TagName("button"), 20)
                .FirstOrDefault(x => x.GetAttribute("aria-label") == "Marka Araştırma"));
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("input[placeholder='Marka İlan Bülten No']"), 20).SendKeys(bulletinNumber);
            Thread.Sleep(2000);
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[2]/main/div[1]/div/div/div[2]/div[2]/div/button[2]"), 20));
            Thread.Sleep(2000);
            driver.WaitAjaxLoad();
        }
        public static void SearchDesigns(this IWebDriver driver, string bulletinNumber)
        {

            driver.ClickWithJs(driver.FindElements(By.TagName("button"), 20)
                .FirstOrDefault(x => x.GetAttribute("aria-label") == "Tasarım Araştırma"));
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("input[placeholder='Bülten Numarası']"), 20).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("input[placeholder='Bülten Numarası']"), 20).SendKeys(bulletinNumber);
            Thread.Sleep(2000);
            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[2]/main/div[1]/div/div/div[2]/div[2]/div/button[2]"), 20));
            Thread.Sleep(2000);
            driver.WaitAjaxLoad();
        }
        public static void SearchPatents(this IWebDriver driver, DateTime startTime, DateTime endTime)
        {

            driver.ClickWithJs(driver.FindElements(By.TagName("button"), 20)
                .FirstOrDefault(x => x.GetAttribute("aria-label") == "Patent Araştırma"));
            Thread.Sleep(2000);
            driver.ClickWithJs(driver.FindElement(By.CssSelector("input[placeholder='Yayın Tarihi']"),20));
            Thread.Sleep(2000);

            driver.SetDate(startTime);

            driver.ClickWithJs(driver.FindElement(By.CssSelector("input[placeholder='Yayın Bitiş Tarihi']"),20));
            Thread.Sleep(2000);

            driver.SetDate(endTime);

            driver.ClickWithJs(driver.FindElement(By.XPath("//*[@id=\"__next\"]/div/div[2]/main/div[1]/div/div/div[2]/div[2]/div/button[2]"), 20));
            Thread.Sleep(2000);
            driver.WaitAjaxLoad();
        }

        public static bool IsDataAccessible(this IWebDriver driver)
        {
            try
            {
                if (!driver.FindElements(By.ClassName("swal2-html-container"), 1)
                        .Any(div => div.Text.Contains("Doğru işlem yaptığınızdan emin olunuz"))) return true;
            }
            catch
            {
                return true;
            }

            try
            {
                driver.ClickWithJs(driver.FindElement(By.ClassName("swal2-confirm")));
            }
            catch
            {
                // ignored
            }

            return false;
       

        }
        private static void SetDate(this IWebDriver driver, DateTime date)
        {
            driver.FindElement(By.ClassName("MuiToolbar-gutters"), 20).FindElements(By.TagName("button"))[0].Click();
            Thread.Sleep(2000);
            foreach (var yearDiv in driver.FindElement(By.ClassName("MuiPickersYearSelection-container"), 20)
                         .FindElements(By.TagName("div")))
            {
                if (yearDiv.Text != date.Year.ToString()) continue;
                driver.ClickWithJs(yearDiv);
                break;
            }

            var switchDiv = driver.FindElement(By.ClassName("MuiPickersCalendarHeader-switchHeader"), 20);
            var decreaseButton = switchDiv.FindElements(By.TagName("button"))[0];
            var increaseButton = switchDiv.FindElements(By.TagName("button"))[1];
            var difference = date.Month - DateTime.Now.Month;
            for (var i = 0; i < (difference < 0 ? difference * -1 : (difference)); i++)
            {
                if (difference < 0)
                {
                    decreaseButton.Click();
                    Thread.Sleep(2000);
                }

                else
                {
                    increaseButton.Click();
                    Thread.Sleep(2000);
                }
                  
            }

            driver.FindElement(By.ClassName("MuiPickersCalendar-transitionContainer"), 20)
                .FindElements(By.TagName("button")).FirstOrDefault(x => x.Text == date.Day.ToString())
                ?.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("MuiPickersModal-withAdditionalAction"), 20)
                .FindElements(By.TagName("button"))[2].Click();
            Thread.Sleep(2000);
        }

    }
}
