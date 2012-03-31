using RightRecruit.Domain.Common;

namespace RightRecruit.Domain.Plan
{
    public class Plan : Entity
    {
        public double? Cost { get; set; }
        public Currency Currency { get; set; }
    }
}