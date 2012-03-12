using System.Collections.Generic;

namespace RightRecruit.Domain.Client.Revenue
{
    public class RevenueModel : Entity
    {
        
    }

    public class LevelBasedRevenueModel : RevenueModel
    {
        public List<LevelToCutMap> LevelToCutMaps { get; set; }
    }

    public class LevelToCutMap : Entity
    {
        public PositionLevel PositionLevel { get; set; }
        public double PercentageCut { get; set; }
    }

    public class CtcRangeBasedRevenueModel : RevenueModel
    {
        public List<CtcToCutMap> CtcToCutMaps { get; set; }
    }

    public class CtcToCutMap : Entity
    {
        public double MinCtc { get; set; }
        public double MaxCtc { get; set; }
        public double PercentageCut { get; set; }
    }
}