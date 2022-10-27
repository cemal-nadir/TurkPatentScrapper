using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.Main
{
   public static class Startup
    {
        public static void SetConfigs()
        {
            RuntimeConfigs.ApplicationStartupPath = System.AppDomain.CurrentDomain.BaseDirectory;
        }
       
    }
}
