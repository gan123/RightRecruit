using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;
using RightRecruit.Mvc.Infrastructure.Emailer;
using RightRecruit.Mvc.Infrastructure.Filters;
using RightRecruit.Mvc.Infrastructure.Infrastructure;
using RightRecruit.Mvc.Infrastructure.Plumbing;
using RightRecruit.Signup.Controllers;

namespace RightRecruit.Signup
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RavenActionFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Home", // Route name
                "home", // URL with parameters
                new { controller = "Home", action = "Home"} // Parameter defaults
            );

            routes.MapRoute(
                "Mail", // Route name
                "mail", // URL with parameters
                new { controller = "Home", action = "Mail" } // Parameter defaults
            );

            routes.MapRoute(
              "Pricing", // Route name
              "pricing", // URL with parameters
              new { controller = "Home", action = "Pricing" } // Parameter defaults
          );

            routes.MapRoute(
               "Signup", // Route name
               "signup", // URL with parameters
               new { controller = "Signup", action = "Signup" } // Parameter defaults
           );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ConventionBasedResolver(container.Kernel));
            container.Register(
                Component.For<IControllerActivator>().ImplementedBy<ControllerActivator>(),
                Component.For<HomeController>().ImplementedBy<HomeController>().LifeStyle.Transient,
                Component.For<SignupController>().ImplementedBy<SignupController>().LifeStyle.Transient,
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>(),
                Component.For<IEmailer>().ImplementedBy<Emailer>(),
                Component.For<HttpSessionStateBase>().LifeStyle.PerWebRequest
                .UsingFactoryMethod(() => new HttpSessionStateWrapper(HttpContext.Current.Session)));

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}