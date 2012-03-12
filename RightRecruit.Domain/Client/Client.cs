using System.Collections.Generic;
using RightRecruit.Domain.Client.Revenue;
using RightRecruit.Domain.Common;
using RightRecruit.Domain.User;

namespace RightRecruit.Domain.Client
{
    public class Client : Entity
    {
        public string AlternateName { get; set; }
        public string AgreementAttachmentId { get; set; }
        public string ClientLogoAttachmentId { get; set; }
        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public DenormalizedReference<ClientUser> Spoc { get; set; }
        public DenormalizedReference<Agency.Agency> Agency { get; set; }
        public List<DenormalizedReference<Position>> Positions { get; set; }
        public DenormalizedReference<RevenueModel> RevenueModel { get; set; }
    }
}