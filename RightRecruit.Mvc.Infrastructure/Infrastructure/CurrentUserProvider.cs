using System.Web;
using RightRecruit.Domain.User;

namespace RightRecruit.Mvc.Infrastructure.Infrastructure
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public User CurrentUser
        {
            get { return (User) HttpContext.Current.Session["CurrentUser"]; }
        }
    }
}