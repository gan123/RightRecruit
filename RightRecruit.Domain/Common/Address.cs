using RightRecruit.Domain.Plan;

namespace RightRecruit.Domain.Common
{
    public class Address : Entity
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Street3 { get; set; }
        public string PinCode { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
    }

    public class Amount : Entity
    {
        public double Value { get; set; }
        public Currency Currency { get; set; }
    }
}