using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;

namespace RightRecruit.Web.Controllers
{
    public class LoginController : AbstractController
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

    }
}
