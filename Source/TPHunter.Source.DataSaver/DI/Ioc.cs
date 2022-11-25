using System.Net.Http;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;
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
                Component.For(typeof(IScrapperClientService<>)).ImplementedBy(typeof(ScrapperClientService<>)).DependsOn(Dependency.OnValue("apiUri",apiUri))
            );
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
   
}
