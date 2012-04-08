using System;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Raven.Client.Linq;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.Plan;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Emailer;
using RightRecruit.Services.Password;
using RightRecruit.Services.Plan;
using RightRecruit.Services.Product;
using RightRecruit.Web.Models;
using RightRecruit.Web.ViewModels;

namespace RightRecruit.Web.Controllers
{
    public class AdminController : AbstractController
    {
        private readonly IEmailer _emailer;
        private readonly IPlanFactory _planFactory;
        private readonly IPassword _password;
        private readonly IProductFactory _productFactory;

        public AdminController(
            IEmailer emailer,
            IPlanFactory planFactory,
            IPassword password,
            IProductFactory productFactory)
        {
            _emailer = emailer;
            _planFactory = planFactory;
            _password = password;
            _productFactory = productFactory;
        }

        public ActionResult Recruiters()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Load()
        {
            var currentUserAgencyId = HttpContext.Session[Globals.Agency].ToString();
            var agency = UnitOfWork.DocumentSession.Load<Agency>(currentUserAgencyId);
            if (agency == null)
                throw new SecurityException("You do not belong to a valid agency");

            var agencyPlan = UnitOfWork.DocumentSession.Query<AgencyPlan>()
                .Where(a => a.Agency.Id == agency.Id)
                .SingleOrDefault();

            if (agencyPlan == null) return Json(new AdminRecruitersViewModel(), JsonRequestBehavior.AllowGet);
            
            var viewModel = new AdminRecruitersViewModel();
            viewModel.Plan = agencyPlan.Plan.Name;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(SavePlanDto savePlan)
        {
            var currentUserAgencyId = HttpContext.Session[Globals.Agency].ToString();
            var agency = UnitOfWork.DocumentSession.Load<Agency>(currentUserAgencyId);
            if (agency == null)
                throw new SecurityException("You do not belong to a valid agency");

            var agencyPlan = UnitOfWork.DocumentSession.Query<AgencyPlan>()
                .Where(a => a.Agency.Id == agency.Id)
                .SingleOrDefault();

            var currentUseId = ((User) HttpContext.Session[Globals.CurrentUser]).Id;
            if (agencyPlan == null) // new
            {
                var plan = _planFactory.CreatePlan(savePlan.Plan, currentUseId);
                var endDate = plan is ThirtyDayTrialPlan ? DateTime.Now.AddDays(30).Date : savePlan.EndDate;
                agencyPlan = _planFactory.CreateAgencyPlan(plan, endDate, agency);
                foreach(var recruiterPlans in savePlan.Recruiters)
                {
                    var generatedPassword = _password.Generate();
                    var recruiter = new Recruiter
                                        {
                                            Agency = agency,
                                            IsManager = recruiterPlans.Role == "Manager",
                                            IsTeamLead = recruiterPlans.Role == "Team Lead",
                                            IsWorkingFromHome = recruiterPlans.Role == "Contract",
                                            Login = recruiterPlans.Name.Split(' ')[0],
                                            Name = recruiterPlans.Name,
                                            Contact = new Contact {Email = recruiterPlans.Email},
                                            HashedPassword = generatedPassword.Password
                                        };
                    UnitOfWork.DocumentSession.Store(recruiter);

                    var recruiterProduct = new RecruiterProduct();
                    recruiterProduct.Cost = recruiterProduct.Cost;
                    recruiterProduct.EndDate = endDate;
                    recruiterProduct.LastUpdatedUserId = currentUseId;
                    recruiterProduct.Product = _productFactory.Get(recruiterPlans.Product);
                    agencyPlan.RecruiterProducts.Add(recruiterProduct);

                    _emailer.SendEmail(recruiter.Contact.Email, "New account", "New account created, username : " + recruiter.Login + ", password : " + generatedPassword.ActualPassword, false);
                }

                UnitOfWork.DocumentSession.Store(agencyPlan);
            }
            return View("Recruiters");
        }
    }
}
