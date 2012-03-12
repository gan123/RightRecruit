using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RightRecruit.Web.Plumbing
{
    public class ControllerActivator : IControllerActivator
    {
        #region IControllerActivator Members

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }

        #endregion
    }
}