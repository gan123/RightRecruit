using System;
using System.Collections.Generic;
using RightRecruit.Domain.Common;

namespace RightRecruit.Domain.User
{
    public class Recruiter : User
    {
        public DenormalizedReference<Agency.Agency> Agency { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsTeamLead { get; set; }
        public bool IsManager { get; set; }
        public bool IsWorkingFromHome { get; set; }
        public List<DenormalizedReference<Target.Target>> Targets { get; set; }
    }

    public class RecruiterIncentives : Entity
    {
        public DenormalizedReference<Recruiter> Recruiter { get; set; }
        public DateTime EarnedDate { get; set; }
        public Amount Commission { get; set; }
    }
}