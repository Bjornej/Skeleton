using Castle.MicroKernel.Context;
using Castle.MicroKernel.Lifestyle.Scoped;

namespace Web.Windsor
{
    public class MsScopedAccesor : IScopeAccessor
    {
        public void Dispose()
        {

        }

        public ILifetimeScope GetScope(CreationContext context)
        {
            return MsLifetimeScope.Current.WindsorLifeTimeScope;
        }
    }
}
