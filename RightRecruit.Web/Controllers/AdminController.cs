using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Web.ViewModels;

namespace RightRecruit.Web.Controllers
{
    public class AdminController : AbstractController
    {
        //
        // GET: /Admin/
        public ActionResult Recruiters()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Load()
        {
            return Json(new AdminRecruitersViewModel(), JsonRequestBehavior.AllowGet);
        }
    }
}
