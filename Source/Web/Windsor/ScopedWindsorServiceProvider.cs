namespace Web.Windsor
{
    using Castle.Windsor;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    public class ScopedWindsorServiceProvider : IServiceProvider, ISupportRequiredService
    {
        private readonly IWindsorContainer _container;

        private readonly MsLifetimeScope _ownMsLifetimeScope;

        public ScopedWindsorServiceProvider(IWindsorContainer container, MsLifetimeScopeProvider msLifetimeScopeProvider)
        {
            _container = container;
            _ownMsLifetimeScope = msLifetimeScopeProvider.LifetimeScope;
        }

        public object GetService(Type serviceType)
        {
            return GetServiceInternal(serviceType, true);
        }

        public object GetRequiredService(Type serviceType)
        {
            return GetServiceInternal(serviceType, false);
        }

        private object GetServiceInternal(Type serviceType, bool isOptional)
        {
            using (MsLifetimeScope.Using(_ownMsLifetimeScope))
            {
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var allObjects = _container.ResolveAll(serviceType.GenericTypeArguments[0]);
                    Array.Reverse(allObjects);
                    return allObjects;
                }

                if (!isOptional)
                {
                    return _container.Resolve(serviceType);
                }

                if (_container.Kernel.HasComponent(serviceType))
                {
                    return _container.Resolve(serviceType);
                }

                return null;
            }
        }
    }
}
