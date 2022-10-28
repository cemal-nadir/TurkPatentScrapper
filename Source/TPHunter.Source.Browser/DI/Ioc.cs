using Browser.Base;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Browser.DI
{
    public static class Ioc
    {
        private static readonly IWindsorContainer container = new WindsorContainer();
        public static void ChromeBaseFactory()
        {
            container.Register(
                Component.For<IBrowserBase>().ImplementedBy<ChromeBase>()
                );
        }
        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
