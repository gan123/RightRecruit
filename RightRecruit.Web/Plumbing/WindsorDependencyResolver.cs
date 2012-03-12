using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;

namespace RightRecruit.Web.Plumbing
{
    public class WindsorDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IWindsorContainer container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            this.container = container;
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            return Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType).Cast<object>();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return container.ResolveAll<TService>();
        }

        public TService GetInstance<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }

        #endregion
        private object Resolve(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}