using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;

namespace RightRecruit.Web.Controllers
{
    public class ClientsController : AbstractController
    {
        
        [HttpGet]
        public ActionResult Clients()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View("NewClient");
        }

        [HttpPost]
        public ActionResult Save()
        {
            return View("Clients");
        }
    }
}
