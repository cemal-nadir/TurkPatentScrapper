using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.Main
{
   public static class Startup
    {
        public static void SetConfigs()
        {
            RuntimeConfigs.ApplicationStartupPath = AppDomain.CurrentDomain.BaseDirectory;
            RuntimeConfigs.DesignRejectedImageLocation = RuntimeConfigs.ApplicationStartupPath + "\\DesignRejectImage.cgn";
            using var streamReader = new StreamReader(RuntimeConfigs.ApplicationStartupPath + "\\config.json");
            RuntimeConfigs.GeneralConfig = JsonConvert.DeserializeObject<GeneralConfig>(streamReader.ReadToEnd());
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            #region Dependency  Injection

            Browser.DI.Ioc.RegisterModules();
            Shared.Scrapper.Ioc.RegisterModules();
            DataSaver.DI.Ioc.RegisterModules();
            ImageProcess.Ioc.RegisterModules();
            Scrapper.DI.Ioc.RegisterModules();

            #endregion

        }
       
    }
}
