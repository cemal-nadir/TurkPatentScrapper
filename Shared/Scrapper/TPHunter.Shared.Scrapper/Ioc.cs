using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;

namespace TPHunter.Shared.Scrapper
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void ApiClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<ApiClient>().Named(nameof(ApiClient))
            );
        }

        public static void TurkPatentClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<TurkPatentClient>().Named(nameof(TurkPatentClient))
            );
        }
        public static void ClientCredentialTokenFactory()
        {
            Container.Register(
                Component.For<IClientCredentialTokenService>().ImplementedBy<ClientCredentialTokenService>().Named(nameof(ClientCredentialTokenService))
            );
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
