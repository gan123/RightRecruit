using System;
using System.Collections.Generic;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.User;

namespace RightRecruit.Domain.Agency
{
    public class Agency : Entity
    {
        public string Website { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public List<DenormalizedReference<AgencyEarnings>> Earnings { get; set; }
    }

    public class AgencyEarnings : Entity
    {
        public DenormalizedReference<Agency> Agency { get; set; }
        public double RevenueEarned { get; set; }
        public DenormalizedReference<Recruiter> RecruiterResponsible { get; set; }
        public DateTime AsOfDate { get; set; }
    }
}