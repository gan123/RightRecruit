using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Raven.Client.Linq;
using RightRecruit.Domain;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.Plan;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Emailer;
using RightRecruit.Mvc.Infrastructure.Result;
using RightRecruit.Mvc.Infrastructure.Utility;
using RightRecruit.Services.Plan;
using RightRecruit.Web.Models;
using RightRecruit.Web.ViewModels;

namespace RightRecruit.Web.Controllers
{
    public class AdminController : AbstractController
    {
        private readonly IEmailer _emailer;
        private readonly IPlanFactory _planFactory;

        public AdminController(
            IEmailer emailer,
            IPlanFactory planFactory)
        {
            _emailer = emailer;
            _planFactory = planFactory;
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

                agencyPlan = _planFactory.CreateAgencyPlan(plan, plan is ThirtyDayTrialPlan ? DateTime.Now.AddDays(30).Date : savePlan.EndDate, agency);
                foreach(var recruiterPlans in savePlan.Recruiters)
                {
                    var recruiter = new Recruiter();
                    recruiter.Agency = agency;
                    recruiter.CreatedDate = DateTime.Now;
                    recruiter.IsManager = recruiterPlans.Role == "Manager";
                    recruiter.IsTeamLead = recruiterPlans.Role == "Team Lead";
                    recruiter.IsWorkingFromHome = recruiterPlans.Role == "Contract";
                    recruiter.Login = recruiterPlans.Name.Split(' ')[0];
                    recruiter.Name = recruiterPlans.Name;
                    recruiter.Contact = new Contact {Email = recruiterPlans.Email};
                    var password = RandomPassword.Generate(8);
                    string salt;
                    string hash;
                    new SaltedHash().GetHashAndSaltString(password, out hash, out salt);
                    recruiter.HashedPassword = new Password(password.ToByteArray(), salt.ToByteArray(), hash.ToByteArray());
                    UnitOfWork.DocumentSession.Store(recruiter);

                    var recruiterProduct = new RecruiterProduct();
                    recruiterProduct.Cost = recruiterProduct.Cost;
                    recruiterProduct.CreatedDate = DateTime.Now;
                    recruiterProduct.EndDate = savePlan.EndDate;
                    recruiterProduct.LastUpdatedDate = DateTime.Now;
                    recruiterProduct.LastUpdatedUserId = currentUseId;
                    recruiterProduct.Product = recruiterPlans.Product == "Basic" ? ProductType.Basic : recruiterPlans.Product == "Pro" ? ProductType.Pro : ProductType.Intermediate;
                    agencyPlan.RecruiterProducts.Add(recruiterProduct);

                    _emailer.SendEmail(recruiter.Contact.Email, "New account", "New account created, username : " + recruiter.Login + ", password : " + password, false);
                }

                UnitOfWork.DocumentSession.Store(agencyPlan);
            }
            return View("Recruiters");
        }
    }
}
