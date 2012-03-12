using System.ComponentModel;

namespace RightRecruit.Domain.Plan
{
    public enum ProductType
    {
        [Description("Basic")]
        Basic = 1,
        [Description("Intermediate")]
        Intermediate = 2,
        [Description("Pro")]
        Pro = 2
    }
}