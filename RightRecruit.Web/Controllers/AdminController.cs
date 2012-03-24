using System.Collections.Generic;
using System.Web.Mvc;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Web.Models;

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

    public class AdminRecruitersViewModel
    {
        public AdminRecruitersViewModel()
        {
            Recruiters = new List<RecruiterLineDto>
                             {
                                 new RecruiterLineDto {Name = "Test"}
                             };
        }

        public IEnumerable<RoleDto> Roles
        {
            get
            {
                yield return new RoleDto {Id = 1, Name = "Recruiter"};
                yield return new RoleDto { Id = 2, Name = "Team Lead" };
                yield return new RoleDto { Id = 3, Name = "Manager" };
                yield return new RoleDto { Id = 4, Name = "Contract" };
            }
        }

        public IEnumerable<PlanDto> Plans
        {
            get
            {
                yield return new PlanDto {Id = 1, Name = "30 day trial"};
                yield return new PlanDto { Id = 2, Name = "Monthly" };
                yield return new PlanDto { Id = 3, Name = "Annual" };
            }
        }

        public IEnumerable<ProductDto> Products
        {
            get
            {
                yield return new ProductDto {Id = 1, Name = "Basic"};
                yield return new ProductDto { Id = 2, Name = "Intermediate" };
                yield return new ProductDto { Id = 3, Name = "Pro" };
            }
        }
        public List<RecruiterLineDto> Recruiters { get; set; }
    }
}
