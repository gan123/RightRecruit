using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;

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
                return View();
            }
            return View();
        }
    }

    public class AgencyDto
    {
        [Required(ErrorMessage = "Can't leave this blank")]
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
