using System.Web.Mvc;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Signup.Models;

namespace RightRecruit.Signup.Controllers
{
    public class SignupController : AbstractController
    {
        //
        // GET: /Signup/
        [HttpGet]
        public ActionResult Signup()
        {
            var model = new AgencyDto();
            return View(model);
        }

        [HttpPost]
        public ActionResult Signup(AgencyDto agency)
        {
            if (ModelState.IsValid)
            {
                var newAgency = new Agency();
                var contact = new Contact {Email = agency.Email, Phone = agency.Phone};
                newAgency.Website = agency.Website;
                newAgency.Name = agency.CompanyName;
                newAgency.Contact = contact;
                UnitOfWork.DocumentSession.Store(newAgency);
                return RedirectToAction("Home", "Home");
            }
            return View();
        }
    }
}
