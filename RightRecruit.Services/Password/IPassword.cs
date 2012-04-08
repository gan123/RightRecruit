namespace RightRecruit.Services.Password
{
    public interface IPassword
    {
        GeneratedPassword Generate();
    }

    public class GeneratedPassword
    {
        public Domain.User.Password Password { get; set; }
        public string ActualPassword { get; set; }
    }
}