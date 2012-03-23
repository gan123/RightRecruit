using System.Linq;
using System.Text;
using System.Web.Mvc;
using Raven.Client.Linq;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Utility;
using RightRecruit.Web.Models;

namespace RightRecruit.Web.Controllers
{
    public class LoginController : AbstractController
    {
        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginDto();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginDto login)
        {
            if (ModelState.IsValid)
            {
                var admins = UnitOfWork.DocumentSession.Query<AgencyAdmin>()
                    .Where(u => u.Login == login.UserName)
                    .ToList();

                if (admins.Any(u => u.HashedPassword.Text.ToPlainString() == login.Password))
                {
                    var user = admins.Single(u => u.HashedPassword.Text.ToPlainString() == login.Password && u.Login == login.UserName);
                    var savedSalt = user.HashedPassword.Salt.ToPlainString();
                    var savedHash = user.HashedPassword.Hash.ToPlainString();
                    if (new SaltedHash().VerifyHashString(login.Password, savedHash, savedSalt))
                    {
                        HttpContext.Session[Globals.CurrentUser] =user;
                        return RedirectToAction("Home", "Home");
                    }
                }
            }
            return View();
        }
    }
}

