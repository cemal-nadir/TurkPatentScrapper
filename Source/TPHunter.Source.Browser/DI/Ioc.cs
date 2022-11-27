using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TPHunter.Source.Browser.Base;

namespace TPHunter.Source.Browser.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();
        public static void ChromeBaseFactory()
        {
            Container.Register(
                Component.For<IBrowserBase>().ImplementedBy<ChromeBase>().Named(nameof(ChromeBase))
                );
        }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
