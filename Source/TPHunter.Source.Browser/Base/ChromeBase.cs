
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Browser.Helpers;

namespace Browser.Base
{
    public class ChromeBase : IBrowserBase
    {
        private readonly ChromeOptions options;
        private ChromeHelper chromeHelper;
        public ChromeBase()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            options = new ChromeOptions();
      //      options.AddArgument("--headless");             
            options.AddArguments("--enable-extensions");
            options.AddArgument("no-sandbox");
            options.AddArgument("--ignore-certificate-errors");
       //     options.AddUserProfilePreference("download.default_directory", Configs.appConfiguration.GetSection("GeneralSettings").GetSection("DownloadLocation").Value);
       //     options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddArgument("disable-infobars");
            options.AddArgument("--disable-setuid-sandbox");
            options.AddArgument("--disable-gpu");
      //      options.AddArguments("--start-maximized");
            options.AddArgument("--user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36");
            options.AddArguments("--window-size=1280,1024");
        }
        public IWebDriver GenerateBrowser()
        {
            chromeHelper = new ChromeHelper();
            var serviceLoc=chromeHelper.FindReleaseService();
            var service=ChromeDriverService.CreateDefaultService(serviceLoc);
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            return new ChromeDriver(service, options);
        }
    }
}
