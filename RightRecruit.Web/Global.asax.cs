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
using RightRecruit.Web.Controllers;
using RightRecruit.Web.Filters;

namespace RightRecruit.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RavenActionFilter());
            filters.Add(new AuthenticationFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Login", // Route name
                "login", // URL with parameters
                new { controller = "Login", action = "Login"} // Parameter defaults
            );

            routes.MapRoute(
                "Home", // Route name
                "home", // URL with parameters
                new { controller = "Home", action = "Home" } // Parameter defaults
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
                Component.For<LoginController>().ImplementedBy<LoginController>().LifeStyle.Transient,
                Component.For<HomeController>().ImplementedBy<HomeController>().LifeStyle.Transient,
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>(),
                Component.For<ICurrentUserProvider>().ImplementedBy<CurrentUserProvider>(),
                Component.For<IEmailer>().ImplementedBy<Emailer>(),
                Component.For<HttpSessionStateBase>().LifeStyle.PerWebRequest
                .UsingFactoryMethod(() => new HttpSessionStateWrapper(HttpContext.Current.Session)));

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}