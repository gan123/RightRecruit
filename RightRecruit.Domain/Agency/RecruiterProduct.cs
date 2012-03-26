using System;
using RightRecruit.Domain.Plan;
using RightRecruit.Domain.User;

namespace RightRecruit.Domain.Agency
{
    public class RecruiterProduct : Entity
    {
        public ProductType Product { get; set; }
        public DenormalizedReference<Recruiter> Recruiter { get; set; }
        public double Cost { get; set; }
        public DateTime EndDate { get; set; }
    }
}