using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace TPHunter.Source.ImageProcess
{
    public static class Ioc
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        public static void RegisterModules()
        {
            ProcessorFactory();
        }
        private static void ProcessorFactory()
        {
            Container.Register(
                Component.For<IProcessor>().ImplementedBy<Processor>().Named(nameof(Processor))
                );
        }
        public static T Resolve<T>(string key)
        {
            return Container.Resolve<T>(key);
        }
    }
}
