using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Registration;

namespace RightRecruit.Web.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)_kernel.Resolve(controllerType);
        }
    }

    public class ViewPageComponentActivator : DefaultComponentActivator
    {
        public ViewPageComponentActivator(ComponentModel model, IKernel kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction)
            : base(model, kernel, onCreation, onDestruction)
        {
        }

        protected override object CreateInstance(CreationContext context, ConstructorCandidate constructor, object[] arguments)
        {
            // Do like the MVC framework.
            var instance = Activator.CreateInstance(context.RequestedType);
            return instance;
        }
    }

    public class WindsorViewPageActivator : IViewPageActivator
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorViewPageActivator"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public WindsorViewPageActivator(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            _kernel = kernel;
        }

        public object Create(ControllerContext controllerContext, Type type)
        {
            if (!_kernel.HasComponent(type.FullName))
            {
                _kernel.Register(Component.For(type).Named(type.FullName).Activator<ViewPageComponentActivator>().LifestyleTransient());
            }
            return _kernel.Resolve(type.FullName, type);
        }
    }
}