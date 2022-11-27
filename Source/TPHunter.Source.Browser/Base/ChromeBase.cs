using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TPHunter.Source.Browser.Helpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TPHunter.Source.Browser.Base
{
    public class ChromeBase : IBrowserBase
    {
        private readonly ChromeOptions _options;
        private readonly ChromeHelper _chromeHelper;
        private IWebDriver _driver;
        public ChromeBase()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _options = new ChromeOptions();
      //      options.AddArgument("--headless");             
            _options.AddArguments("--enable-extensions");
            _options.AddArgument("no-sandbox");
            _options.AddArgument("--ignore-certificate-errors");
     
            _options.AddArgument("disable-infobars");
            _options.AddArgument("--disable-setuid-sandbox");
            _options.AddArgument("--disable-gpu");
      //      options.AddArguments("--start-maximized");
            _options.AddArgument("--user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36");
            _options.AddArguments("--window-size=1280,1024");
            _chromeHelper = new ChromeHelper();
        }

        public IWebDriver Browser => _driver ??= GenerateBrowser();

        public void DisposeBrowser()
        {
            if (_driver == null) return;
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();

        }
        private IWebDriver GenerateBrowser()
        {
            var serviceLoc=_chromeHelper.FindReleaseService();
            var service=ChromeDriverService.CreateDefaultService(serviceLoc);
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            return new ChromeDriver(service, _options);
        }
    }
}
