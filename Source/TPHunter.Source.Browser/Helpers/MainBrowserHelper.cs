using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Browser.Helpers
{
    public static class MainBrowserHelper
    {
        public static void OpenNewTab(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
        public static void SwitchTab(this IWebDriver driver, int tabNumber)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabNumber]);
        }
        public static void SwitchFirstTab(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }
        public static void SwitchLastTab(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
        public static void CloseCurrentTab(this IWebDriver driver)
        {
            driver.Close();
        }
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        public static IEnumerable<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
            return driver.FindElements(by);
        }
        public static void ClickWithJs(this IWebDriver webDriver, IWebElement webElement)
        {

            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }
        public static void PressEsc(this IWebDriver driver)
        {
            driver.FindElement(By.XPath("//body")).SendKeys(Keys.Escape);
        }
        public static string GetXPath(this IWebDriver driver, IWebElement element)
        {
            return (string)((IJavaScriptExecutor)driver).ExecuteScript("gPt=function(c){if(c.id!==''){return'id(\"'+c.id+'\")'}if(c===document.body){return c.tagName}var a=0;var e=c.parentNode.childNodes;for(var b=0;b<e.length;b++){var d=e[b];if(d===c){return gPt(c.parentNode)+'/'+c.tagName+'['+(a+1)+']'}if(d.nodeType===1&&d.tagName===c.tagName){a++}}};return gPt(arguments[0]).toLowerCase();", element);
        }
    }
}
