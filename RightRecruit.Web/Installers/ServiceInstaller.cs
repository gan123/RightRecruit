using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RightRecruit.Services.Plan;

namespace RightRecruit.Web.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPlanFactory>().ImplementedBy<PlanFactory>());
        }
    }
}