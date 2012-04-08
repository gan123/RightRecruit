using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;

namespace RightRecruit.Web.Controllers
{
    public class RecruiterController : AbstractController
    {
        //
        // GET: /Recruiter/

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Inbox()
        {
            return View();
        }
    }
}
