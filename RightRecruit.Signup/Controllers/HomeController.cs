using System.Net;
using System.Net.Mail;
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

        [HttpPost]
        public ActionResult Mail()
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("rightrecruit.mailer@gmail.com", "rightrecruit2012"),
                EnableSsl = true
            };
            client.Send("rightrecruit.mailer@gmail.com", "ganesh.shivshankar@gmail.com", "test", "testbody");
            return View("Home");
        }
    }
}
