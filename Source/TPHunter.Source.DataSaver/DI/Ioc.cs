using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TPHunter.Source.DataSaver.Abstract;
using TPHunter.Source.DataSaver.Concrete;

namespace TPHunter.Source.DataSaver.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void ScrapperClientFactory(string apiUri)
        {
            Container.Register(
                Component.For(typeof(IScrapperClientService<>)).ImplementedBy(typeof(ScrapperClientService<>))
                    .DependsOn(Dependency.OnValue("apiUri", apiUri)).Named("ScrapperClientService")
            );
        }
        public static void ScrapperClientHelperFactory()
        {
            Container.Register(
                Component.For(typeof(IScrapperClientHelperService)).ImplementedBy(typeof(ScrapperClientHelperService)).Named(nameof(ScrapperClientHelperService))
            );
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
   
}
