using RightRecruit.Domain.Common;

namespace RightRecruit.Domain.Plan
{
    public class Plan : Entity
    {
        public ProductType ProductType { get; set; }
        public double? Cost { get; set; }
        public Currency Currency { get; set; }
    }
}