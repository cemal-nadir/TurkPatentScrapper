using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Services.Main;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer container = new WindsorContainer();
        public static void MarkaWorkerFactory()
        {
            container.Register(
                Component.For<IWorker>().ImplementedBy<MarkaWorker>()
                );
        }

        public static void MarkaPageFactory(IWebDriver driver)
        {
            container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(MarkaPage)).DependsOn(Dependency.OnValue("webDriver", driver)));
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
