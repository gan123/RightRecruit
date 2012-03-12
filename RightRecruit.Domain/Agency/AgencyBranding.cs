using System;

namespace RightRecruit.Domain.Agency
{
    public class AgencyBranding : Entity
    {
        public DenormalizedReference<Agency> Agency { get; set; }
        public Theme Theme { get; set; }
        public string AgencyLogoAttachmentId { get; set; }
        public Uri DefaultHomePageUrl { get; set; }
    }
}