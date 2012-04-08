using RightRecruit.Mvc.Infrastructure.Utility;

namespace RightRecruit.Services.Password
{
    public class Password : IPassword
    {
        public GeneratedPassword Generate()
        {
            var password = RandomPassword.Generate(8);
            string salt;
            string hash;
            new SaltedHash().GetHashAndSaltString(password, out hash, out salt);
            return new GeneratedPassword
                       {
                           Password = new Domain.User.Password(password.ToByteArray(), salt.ToByteArray(), hash.ToByteArray()),
                           ActualPassword = password
                       };
        }
    }
}