using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RightRecruit.Signup.Filters;

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
        }
    }
}