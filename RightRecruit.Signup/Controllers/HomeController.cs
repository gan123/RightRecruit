using System.Web.Mvc;

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
