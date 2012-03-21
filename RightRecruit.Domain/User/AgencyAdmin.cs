namespace RightRecruit.Domain.User
{
    public class AgencyAdmin : User
    {
        public DenormalizedReference<Agency.Agency> Agency { get; set; }
    }
}