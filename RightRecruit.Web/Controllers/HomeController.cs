using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;

namespace RightRecruit.Web.Controllers
{
    public class HomeController : AbstractController
    {
        //
        // GET: /Home/

        public ActionResult Home()
        {
            return View();
        }

    }
}
