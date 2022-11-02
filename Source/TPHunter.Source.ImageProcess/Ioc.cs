using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.ImageProcess
{
    public static class Ioc
    {
        private static readonly IWindsorContainer container = new WindsorContainer();
      
        public static void ProcessorFactory()
        {
            container.Register(
                Component.For<IProcessor>().ImplementedBy<Processor>()
                );
        }
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
