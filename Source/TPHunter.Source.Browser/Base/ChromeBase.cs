using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly List<WebDriverModel> _webDriverModels=new();
        public ChromeBase()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _options = new ChromeOptions();
         //   _options.AddArgument("--headless");             
            _options.AddArguments("--enable-extensions");
            _options.AddArgument("no-sandbox");
            _options.AddArgument("--ignore-certificate-errors");
     
            _options.AddArgument("disable-infobars");
            _options.AddArgument("--disable-setuid-sandbox");
            _options.AddArgument("--disable-gpu");
           // _options.AddArguments("--start-maximized");
            _options.AddArgument("--user-agent=Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36");
            _options.AddArguments("--window-size=1280,1024");
           
        }
        public IWebDriver Browser(Guid driverId)
        {
            var driverModel=_webDriverModels.FirstOrDefault(x => x.DriverId == driverId);
            if (driverModel is not null) return driverModel.Driver;
            driverModel = new WebDriverModel()
            {
                Driver = GenerateBrowser(),
                DriverId = driverId

            };
            _webDriverModels.Add(driverModel);
            return driverModel.Driver;
        }
       

        public void DisposeBrowser(Guid driverId)
        {
            var driverModel = _webDriverModels.FirstOrDefault(x => x.DriverId == driverId);
            if (driverModel == null) return;
            driverModel.Driver.Close();
            driverModel.Driver.Quit();
            driverModel.Driver.Dispose();
            _webDriverModels.Remove(driverModel);
        }
        private IWebDriver GenerateBrowser()
        {
            var serviceLoc=ChromeHelper.FindReleaseService();
            var service=ChromeDriverService.CreateDefaultService(serviceLoc);
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            return new ChromeDriver(service, _options);
        }
        private class WebDriverModel
        {
            public Guid DriverId { get; set; }
            public IWebDriver Driver { get; set; }

        }
    }
}
