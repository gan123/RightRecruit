using System.Web;
using RightRecruit.Domain;
using RightRecruit.Domain.User;

namespace RightRecruit.Web.Infrastructure
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public User CurrentUser
        {
            get { return (User) HttpContext.Current.Session["CurrentUser"]; }
        }
    }
}