using System;
using System.Collections.Generic;
using RightRecruit.Domain.User;

namespace RightRecruit.Domain.Agency
{
    public class AgencyPlan : Entity
    {
        public DenormalizedReference<Agency> Agency { get; set; }
        public List<DenormalizedReference<Recruiter>> Recruiters { get; set; }
        public DenormalizedReference<Plan.Plan> Plan { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
    }
}