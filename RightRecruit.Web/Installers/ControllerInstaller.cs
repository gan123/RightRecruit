using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RightRecruit.Web.Controllers;

namespace RightRecruit.Web.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<LoginController>().ImplementedBy<LoginController>().LifeStyle.Transient,
                Component.For<HomeController>().ImplementedBy<HomeController>().LifeStyle.Transient,
                Component.For<SignupController>().ImplementedBy<SignupController>().LifeStyle.Transient,
                Component.For<AdminController>().ImplementedBy<AdminController>().LifeStyle.Transient,
                Component.For<RecruiterController>().ImplementedBy<RecruiterController>().LifeStyle.Transient,
                Component.For<ClientsController>().ImplementedBy<ClientsController>().LifeStyle.Transient);
        }
    }
}