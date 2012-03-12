namespace RightRecruit.Domain.User
{
    public class ClientUser : User
    {
        public DenormalizedReference<Client.Client> Client { get; set; }
    }
}