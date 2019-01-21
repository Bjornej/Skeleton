using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Windsor
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Castle.Windsor;

    public class WindsorServiceScope : IServiceScope
    {

        public IServiceProvider ServiceProvider { get; }

        public MsLifetimeScope LifetimeScope { get; }


        private readonly MsLifetimeScope _parentLifetimeScope;

        public WindsorServiceScope(IWindsorContainer container, MsLifetimeScope currentMsLifetimeScope)
        {
            _parentLifetimeScope = currentMsLifetimeScope;

            LifetimeScope = new MsLifetimeScope();

            _parentLifetimeScope?.AddChild(LifetimeScope);

            using (MsLifetimeScope.Using(LifetimeScope))
            {
                ServiceProvider = container.Resolve<IServiceProvider>();
            }
        }

        public void Dispose()
        {
            _parentLifetimeScope?.RemoveChild(LifetimeScope);
            LifetimeScope.Dispose();
        }
    }
}
