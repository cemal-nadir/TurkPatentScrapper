using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TPHunter.Source.Core.Configs;

namespace TPHunter.Source.FileStorage.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer container = new WindsorContainer();
        public static void AmazonStorageFactory()
        {
            container.Register(
                Component.For<IFileTransferManager<AmazonS3Config>>().ImplementedBy<AmazonStorage>()
                );
        }
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
