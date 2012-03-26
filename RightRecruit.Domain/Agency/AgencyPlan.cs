using System;
using System.Collections.Generic;

namespace RightRecruit.Domain.Agency
{
    public class AgencyPlan : Entity
    {
        public DenormalizedReference<Agency> Agency { get; set; }
        public List<DenormalizedReference<RecruiterProduct>> RecruiterProducts { get; set; }
        public DenormalizedReference<Plan.Plan> Plan { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }
    }
}