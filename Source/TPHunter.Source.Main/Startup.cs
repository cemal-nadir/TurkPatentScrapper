using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
        }
       
    }
}
