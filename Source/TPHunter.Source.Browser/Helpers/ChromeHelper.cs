using System;
using System.IO;
using System.Linq;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.Browser.Helpers
{
    public class ChromeHelper
    {
        public string FindReleaseService()
        {
            try
            {
              
                var directories = new DirectoryInfo(RuntimeConfigs.ApplicationStartupPath+"Chrome").EnumerateDirectories()
                    .OrderByDescending(d => d.CreationTime)
                    .Select(d => d.Name)
                    .ToList();
                foreach (var directory in directories)
                {
                    var serviceLocation = Directory.GetDirectories(RuntimeConfigs.ApplicationStartupPath + "Chrome\\"+directory).FirstOrDefault();
                    if (serviceLocation != null)
                        return serviceLocation;
                }

                throw new Exception("Servis Bulunamadı");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
