using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RightRecruit.Mvc.Infrastructure.Emailer;
using RightRecruit.Raven.Database;
using RightRecruit.Services.Password;
using RightRecruit.Services.Plan;
using RightRecruit.Services.Product;

namespace RightRecruit.Web.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPlanFactory>().ImplementedBy<PlanFactory>(),
                Component.For<IEmailer>().ImplementedBy<Emailer>(),
                Component.For<IPassword>().ImplementedBy<Password>(),
                Component.For<IProductFactory>().ImplementedBy<ProductFactory>(),
                Component.For<IDatabase>().ImplementedBy<Database>());
        }
    }
}