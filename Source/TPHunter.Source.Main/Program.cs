using System;
using TPHunter.Source.Scrapper.Abstract.Main;

namespace TPHunter.Source.Main
{
    internal static class Program
    {
        private static void Main()
        {
            Startup.SetConfigs();
            Scrapper.DI.Ioc.MarkaWorkerFactory();
            Scrapper.DI.Ioc.Resolve<IWorker>().Download().Wait();
            Console.ReadKey();
        }
    }
}
