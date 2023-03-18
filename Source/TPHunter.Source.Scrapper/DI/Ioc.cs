using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Scrapper.Abstract.Shared;
using TPHunter.Source.Scrapper.Services.Shared;

namespace TPHunter.Source.Scrapper.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        public static void RegisterModules()
        {
            MarkaPageFactory();
            DesignPageFactory();
            PatentPageFactory();
            //WorkerFactory<MarkModel>();
            //WorkerFactory<PatentModel>();
            //WorkerFactory<DesignModel>();
            TurkPatentClientServiceFactory();
        }
        //private static void WorkerFactory<TModel>() where TModel :class, IModel
        //{
        //    if (typeof(TModel).IsAssignableFrom(typeof(MarkModel)))
        //    {
        //        Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker<TModel>))
        //            .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServiceTrademark")))
        //            .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<TModel>>(nameof(MarkaPage))))
        //            .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>(nameof(ChromeBase)))).Named("MarkWorker")
        //            .LifestylePooled()
        //        );
        //    }
        //    else if (typeof(TModel).IsAssignableFrom(typeof(PatentModel)))
        //    {
        //        Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker<TModel>))
        //            .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServicePatent")))
        //            .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<TModel>>(nameof(PatentPage))))
        //            .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>(nameof(ChromeBase)))).Named("PatentWorker")
        //            .LifestylePooled()
        //        );
        //    }
        //    else if (typeof(TModel).IsAssignableFrom(typeof(DesignModel)))
        //    {
           
           
        //        Container.Register(Component.For(typeof(IWorker)).ImplementedBy(typeof(Worker<TModel>))                                      
        //            .DependsOn(Dependency.OnValue("scrapperClientService", DataSaver.DI.Ioc.Resolve<IScrapperClientService<TModel>>("ScrapperClientServiceDesign")))
        //            .DependsOn(Dependency.OnValue("pageService", Resolve<IPage<TModel>>(nameof(DesignPage))))
        //            .DependsOn(Dependency.OnValue("browserBase", Browser.DI.Ioc.Resolve<IBrowserBase>(nameof(ChromeBase)))).Named("DesignWorker")
        //            .LifestylePooled()
        //        );
        //    }
        //}
        private static void TurkPatentClientServiceFactory()
        {
            Container.Register(Component.For(typeof(ITurkPatentClientService)).ImplementedBy(typeof(TurkPatentClientService)).Named(nameof(TurkPatentClientService)));
        }
        private static void MarkaPageFactory()
        {
            Container.Register(Component.For(typeof(IPage<MarkModel>)).ImplementedBy(typeof(MarkaPage)).Named(nameof(MarkaPage)).LifestylePooled()
                );
        }

        private static void DesignPageFactory()
        {
            Container.Register(Component.For(typeof(IPage<DesignModel>)).ImplementedBy(typeof(DesignPage)).Named(nameof(DesignPage)).LifestylePooled()
                );
        }

        private static void PatentPageFactory()
        {
            Container.Register(Component.For(typeof(IPage<PatentModel>)).ImplementedBy(typeof(PatentPage)).Named(nameof(PatentPage)).LifestylePooled()
               );
        }

        public static T Resolve<T>(string key)
        {
            return Container.Resolve<T>(key);
        }
    }
}
