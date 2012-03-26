using System.Collections.Generic;
using RightRecruit.Domain.Common;

namespace RightRecruit.Domain.Agency
{
    public class Agency : Entity
    {
        public string Website { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public List<DenormalizedReference<AgencyEarnings>> Earnings { get; set; }
    }
}