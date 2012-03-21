using System.Web.Mvc;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Utility;
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

                var adminUser = new AgencyAdmin();
                adminUser.Name = "Admin";
                adminUser.Contact = newAgency.Contact;
                adminUser.Address = newAgency.Address;
                adminUser.Login = "admin";
                var password = RandomPassword.Generate(8);
                string salt;
                string hash;
                new SaltedHash().GetHashAndSaltString(password, out hash, out salt);
                adminUser.HashedPassword = new Password(password.ToByteArray(), salt.ToByteArray(), hash.ToByteArray());
                adminUser.Agency = newAgency;
                UnitOfWork.DocumentSession.Store(adminUser);

                return RedirectToAction("Home", "Home");
            }
            return View();
        }
    }
}
