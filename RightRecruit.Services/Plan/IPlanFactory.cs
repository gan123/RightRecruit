using System;
using System.Collections.Generic;
using RightRecruit.Domain;
using RightRecruit.Domain.Agency;
using RightRecruit.Domain.Plan;

namespace RightRecruit.Services.Plan
{
    public interface IPlanFactory
    {
        Domain.Plan.Plan CreatePlan(string type, string userId);
        AgencyPlan CreateAgencyPlan(Domain.Plan.Plan plan, DateTime endDate, Agency agency);
    }

    public class PlanFactory : IPlanFactory
    {
        public Domain.Plan.Plan CreatePlan(string type, string userId)
        {
            switch (type)
            {
                case "Trial":
                    return new ThirtyDayTrialPlan
                    {
                        CreatedDate = DateTime.Now,
                        Currency = Domain.Common.Currency.INR,
                        LastUpdatedDate = DateTime.Now,
                        LastUpdatedUserId = userId
                    };
                case "Monthly":
                    return new MonthlyPlan
                    {
                        CreatedDate = DateTime.Now,
                        Currency = Domain.Common.Currency.INR,
                        LastUpdatedDate = DateTime.Now,
                        LastUpdatedUserId = userId
                    };
                case "Annual":
                    return new AnnualPlan
                    {
                        CreatedDate = DateTime.Now,
                        Currency = Domain.Common.Currency.INR,
                        LastUpdatedDate = DateTime.Now,
                        LastUpdatedUserId = userId
                    };
                default:
                    return null;
            }
        }

        public AgencyPlan CreateAgencyPlan(Domain.Plan.Plan plan, DateTime endDate, Agency agency)
        {
            return new AgencyPlan
                       {
                           Plan = plan,
                           Agency = agency,
                           PlanEndDate = endDate,
                           RecruiterProducts = new List<DenormalizedReference<RecruiterProduct>>()
                       };
        }
    }
}