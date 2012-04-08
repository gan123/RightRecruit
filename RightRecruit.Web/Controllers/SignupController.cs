using System.Web.Mvc;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Emailer;
using RightRecruit.Mvc.Infrastructure.Utility;
using RightRecruit.Raven.Database;
using RightRecruit.Services.Password;
using RightRecruit.Web.Models;
using Password = RightRecruit.Domain.User.Password;

namespace RightRecruit.Web.Controllers
{
    public class SignupController : AbstractController
    {
        private readonly IEmailer _emailer;
        private readonly IPassword _password;
        private readonly IDatabase _database;

        public SignupController(
            IEmailer emailer,
            IPassword password,
            IDatabase database)
        {
            _emailer = emailer;
            _password = password;
            _database = database;
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Proceed(AgencyDto agency)
        {
            if (!string.IsNullOrEmpty(agency.CompanyName))
            {
                var newAgency = new Agency();
                var contact = new Contact { Email = agency.Email, Phone = agency.Phone };
                newAgency.Website = agency.Website;
                newAgency.Name = agency.CompanyName;
                newAgency.Contact = contact;
                UnitOfWork.DocumentSession.Store(newAgency);

                var generatedPassword = _password.Generate();
                var adminUser = new AgencyAdmin();
                adminUser.Name = agency.AdminId;
                adminUser.Contact = newAgency.Contact;
                adminUser.Address = newAgency.Address;
                adminUser.Login = agency.AdminId;
                adminUser.HashedPassword = generatedPassword.Password;
                adminUser.Agency = newAgency;
                UnitOfWork.DocumentSession.Store(adminUser);

                _emailer.SendEmail(agency.Email, "Account", "username : " + adminUser.Name + ", password: " + generatedPassword.ActualPassword, false);

                return new EmptyResult();
            }
            return View("Signup");
        }
    }
}
