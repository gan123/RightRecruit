using RightRecruit.Domain.User;

namespace RightRecruit.Signup.Infrastructure
{
    public interface ICurrentUserProvider
    {
        User CurrentUser { get; }
    }
}