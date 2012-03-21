using RightRecruit.Domain.Common;

namespace RightRecruit.Domain.User
{
    public class User : Entity
    {
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public string Login { get; set; }
        public Password HashedPassword { get; set; }
    }
}