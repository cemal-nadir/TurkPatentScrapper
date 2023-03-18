using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;

namespace TPHunter.Shared.Scrapper
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        public static void RegisterModules()
        {
            IdentityClientFactory();
            ClientCredentialTokenFactory();
            ApiClientFactory();
            TurkPatentClientFactory();
          
        }
        private static void ApiClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<ApiClient>().Named(nameof(ApiClient))
            );
        }
        private static void IdentityClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<IdentityClient>().Named(nameof(IdentityClient))
            );
        }

        private static void TurkPatentClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<TurkPatentClient>().Named(nameof(TurkPatentClient))
            );
        }
        private static void ClientCredentialTokenFactory()
        {
            Container.Register(
                Component.For<IClientCredentialTokenService>().ImplementedBy<ClientCredentialTokenService>().Named(nameof(ClientCredentialTokenService))
            );
        }
        public static T Resolve<T>(string key)
        {
            return Container.Resolve<T>(key);
        }
    }
}
