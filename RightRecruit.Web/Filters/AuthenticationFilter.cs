using System.Web.Mvc;
using System.Web.Routing;
using RightRecruit.Mvc.Infrastructure;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Web.Controllers;

namespace RightRecruit.Web.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as AbstractController;
            if (controller != null && controller.CurrentUserProvider.CurrentUser == null &&
                !(filterContext.Controller is LoginController || filterContext.Controller is SignupController || filterContext.Controller is HomeController))
            {
                filterContext.Result = new RedirectToRouteResult("login", new RouteValueDictionary());
            }
        }
    }
}