using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TPHunter.Shared.Scrapper.Models;
using TPHunter.Source.Core.Configs;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.DataSaver.Concrete;

namespace TPHunter.Source.DataSaver.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        public static void RegisterModules()
        {
            ScrapperClientFactory();
            ScrapperClientHelperFactory();
        }

        private static void ScrapperClientFactory()
        {
            Container.Register(
                Component.For(typeof(IScrapperClientService<MarkModel>)).ImplementedBy(typeof(ScrapperClientService))
                    .DependsOn(Dependency.OnValue("apiUri", $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Trademark/")).Named("ScrapperClientServiceTrademark").LifestylePerThread()
            );
            Container.Register(
                Component.For(typeof(IScrapperClientService<PatentModel>)).ImplementedBy(typeof(ScrapperClientService))
                    .DependsOn(Dependency.OnValue("apiUri", $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Patent/")).Named("ScrapperClientServicePatent").LifestylePerThread()
            );
            Container.Register(
                Component.For(typeof(IScrapperClientService<DesignModel>)).ImplementedBy(typeof(ScrapperClientService))
                    .DependsOn(Dependency.OnValue("apiUri", $"{RuntimeConfigs.GeneralConfig.Services.ScrapApiUri}/Api/Design/")).Named("ScrapperClientServiceDesign").LifestylePerThread()
            );
        }
        private static void ScrapperClientHelperFactory()
        {
            Container.Register(
                Component.For(typeof(IScrapperClientHelperService)).ImplementedBy(typeof(ScrapperClientHelperService)).Named(nameof(ScrapperClientHelperService)).LifestylePerThread()
            );
        }
        public static T Resolve<T>(string key)
        {
            return Container.Resolve<T>(key);
        }
    }
   
}
