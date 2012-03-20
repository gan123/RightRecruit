using RightRecruit.Domain.User;

namespace RightRecruit.Mvc.Infrastructure.Infrastructure
{
    public interface ICurrentUserProvider
    {
        User CurrentUser { get; }
    }
}