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
using RightRecruit.Web.Installers;

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

            routes.MapRoute(
               "SignuProceed", // Route name
               "signup/proceed", // URL with parameters
               new { controller = "Signup", action = "Proceed" } // Parameter defaults
           );

            routes.MapRoute(
              "AdminRecruiters", // Route name
              "admin/recruiters", // URL with parameters
              new { controller = "Admin", action = "Recruiters" } // Parameter defaults
          );

            routes.MapRoute(
              "AdminRecruitersLoad", // Route name
              "admin/recruiters/load", // URL with parameters
              new { controller = "Admin", action = "Load" } // Parameter defaults
          );

            routes.MapRoute(
              "AdminRecruitersSave", // Route name
              "admin/recruiters/save", // URL with parameters
              new { controller = "Admin", action = "Save" } // Parameter defaults
          );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ConventionBasedResolver(container.Kernel));
            container.Install(
                new MvcInstaller(),
                new ControllerInstaller(),
                new ServiceInstaller());

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}