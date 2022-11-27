using OpenQA.Selenium;

namespace TPHunter.Source.Browser.Base
{
    public interface IBrowserBase
    {
        public IWebDriver Browser { get; }
        public void DisposeBrowser();
    }
}
