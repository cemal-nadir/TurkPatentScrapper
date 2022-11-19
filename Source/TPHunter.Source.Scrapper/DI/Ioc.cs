using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OpenQA.Selenium;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Services.Main;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void MarkaWorkerFactory()
        {
            Container.Register(
                Component.For<IWorker>().ImplementedBy<MarkaWorker>()
                );
        }
        public static void DesignWorkerFactory()
        {
            Container.Register(
                Component.For<IWorker>().ImplementedBy<DesignWorker>()
                );
        }
        public static void PatentWorkerFactory()
        {
            Container.Register(
                Component.For<IWorker>().ImplementedBy<PatentWorker>()
                );
        }
        public static void MarkaPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(MarkaPage)).DependsOn(Dependency.OnValue("webDriver", driver)));
        }
        public static void DesignPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(DesignPage)).DependsOn(Dependency.OnValue("webDriver", driver)));
        }
        public static void PatentPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(PatentPage)).DependsOn(Dependency.OnValue("webDriver", driver)));
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
