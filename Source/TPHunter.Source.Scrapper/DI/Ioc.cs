using Castle.MicroKernel.Registration;
using Castle.Windsor;
using OpenQA.Selenium;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Browser.Base;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.Scrapper.Abstract.Main;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Services.Main;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void WorkerFactory<TModel>() where TModel : IModel
        {
            Browser.DI.Ioc.ChromeBaseFactory();
            if (typeof(TModel).IsAssignableFrom(typeof(MarkModel)))
            {
                DataSaver.DI.Ioc.ScrapperClientFactory($"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Trademark/");
                MarkaPageFactory(Browser.DI.Ioc.Resolve<IBrowserBase>().Browser);
                Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker))
                    .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<MarkModel>>()))
                    .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>()))
                    .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<MarkModel>>())).Named("MarkWorker")
                );
            }
            else if (typeof(TModel).IsAssignableFrom(typeof(PatentModel)))
            {
                DataSaver.DI.Ioc.ScrapperClientFactory($"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Patent/");
                PatentPageFactory(Browser.DI.Ioc.Resolve<IBrowserBase>().Browser);
                Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker))
                    .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<PatentModel>>()))
                    .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>()))
                    .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<PatentModel>>())).Named("PatentWorker")
                );
            }
            else if (typeof(TModel).IsAssignableFrom(typeof(DesignModel)))
            {
                DataSaver.DI.Ioc.ScrapperClientFactory($"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Design/");
                DesignPageFactory(Browser.DI.Ioc.Resolve<IBrowserBase>().Browser);
                Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker))
                    .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<DesignModel>>()))
                    .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>()))
                    .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<DesignModel>>())).Named("DesignWorker")
                );
            }
        }
        public static void TurkPatentClientServiceFactory()
        {
            Container.Register(Component.For(typeof(ITurkPatentClientService)).ImplementedBy(typeof(TurkPatentClientService)).Named(nameof(TurkPatentClientService)));
        }
        private static void MarkaPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(MarkaPage)).Named(nameof(MarkaPage))
                .DependsOn(Dependency.OnValue("webDriver", driver)));
        }

        private static void DesignPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(DesignPage)).Named(nameof(DesignPage))
                .DependsOn(Dependency.OnValue("webDriver", driver)));
        }

        private static void PatentPageFactory(IWebDriver driver)
        {
            Container.Register(Component.For(typeof(IPage<>)).ImplementedBy(typeof(PatentPage)).Named(nameof(PatentPage))
                .DependsOn(Dependency.OnValue("webDriver", driver)));
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
