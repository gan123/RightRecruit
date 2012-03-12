using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace RightRecruit.Web.Plumbing
{
    public class ConventionBasedResolver : ISubDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IDictionary<DependencyModel, string> _knownDependencies = new Dictionary<DependencyModel, string>();

        public ConventionBasedResolver(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            string componentName;
            if (!_knownDependencies.TryGetValue(dependency, out componentName))
            {
                componentName = dependency.DependencyKey;
            }
            return _kernel.Resolve(componentName, dependency.TargetType);
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            if (_knownDependencies.ContainsKey(dependency))
                return true;

            var handlers = _kernel.GetHandlers(dependency.TargetType);

            //if there's just one, we're not interested.
            if (handlers.Length < 2)
                return false;
            foreach (var handler in handlers)
            {
                if (IsMatch(handler.ComponentModel, dependency) && handler.CurrentState == HandlerState.Valid)
                {
                    if (!handler.ComponentModel.Name.Equals(dependency.DependencyKey, StringComparison.Ordinal))
                    {
                        _knownDependencies.Add(dependency, handler.ComponentModel.Name);
                    }
                    return true;
                }
            }
            return false;
        }

        private static bool IsMatch(ComponentModel model, DependencyModel dependency)
        {
            return dependency.DependencyKey.Equals(model.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}