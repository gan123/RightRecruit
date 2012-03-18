using System.Web;
using RightRecruit.Domain.User;

namespace RightRecruit.Signup.Infrastructure
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public User CurrentUser
        {
            get { return (User) HttpContext.Current.Session["CurrentUser"]; }
        }
    }
}