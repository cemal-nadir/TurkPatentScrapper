using System;
using TPHunter.Source.Scrapper.Abstract.Main;

namespace TPHunter.Source.Main
{
    class Program
    {
      

        static void Main(string[] args)
        {
            Startup.SetConfigs();
            Scrapper.DI.Ioc.MarkaWorkerFactory();
            Scrapper.DI.Ioc.Resolve<IWorker>().Download().Wait();
            Console.ReadKey();
        }
    }
}
