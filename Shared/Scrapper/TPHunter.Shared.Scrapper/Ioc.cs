using Castle.Windsor;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using TPHunter.Shared.Scrapper.Abstracts;
using TPHunter.Shared.Scrapper.Handlers;

namespace TPHunter.Shared.Scrapper
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void HttpClientFactory()
        {
            Container.Register(
                Component.For<IApiClient>().ImplementedBy<ApiClient>()
            );
        }
        public static void ClientCredentialTokenFactory()
        {
            Container.Register(
                Component.For<IClientCredentialTokenService>().ImplementedBy<ClientCredentialTokenService>()
            );
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
