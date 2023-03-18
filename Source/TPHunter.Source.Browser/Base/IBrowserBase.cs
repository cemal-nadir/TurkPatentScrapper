using System;
using OpenQA.Selenium;

namespace TPHunter.Source.Browser.Base
{
    public interface IBrowserBase
    {
        public IWebDriver Browser(Guid driverId);
        public void DisposeBrowser(Guid driverId);
    }
}
