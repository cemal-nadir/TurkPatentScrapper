using Browser.Base;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHunter.Source.Contract
{
    public static class Ioc
    {
        private static readonly IWindsorContainer container = new WindsorContainer();
        public static void A4TechFactory()
        {
            container.Register(
                Component.For<IBrowserBase>().ImplementedBy<ChromeBase>()
                );
        }

        public static void LogitechFactory()
        {
            container.Register(
                Component.For<IMouse>().ImplementedBy<LogitechMouse>()
                );
        }

        public static T ResolveMouse<T>()
        {
            return container.Resolve<T>();
        }
    }
}
