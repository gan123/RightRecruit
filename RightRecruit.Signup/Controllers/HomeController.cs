using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;

namespace RightRecruit.Signup.Controllers
{
    public class HomeController : AbstractController
    {
        //
        // GET: /Home/

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Pricing()
        {
            return View();
        }

    }
}
