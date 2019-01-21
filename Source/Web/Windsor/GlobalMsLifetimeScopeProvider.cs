using System;

namespace Web.Windsor
{
    public class GlobalMsLifetimeScopeProvider : IDisposable
    {
        public MsLifetimeScope LifetimeScope { get; }

        public GlobalMsLifetimeScopeProvider()
        {
            LifetimeScope = new MsLifetimeScope();
        }

        public void Dispose()
        {
            LifetimeScope.Dispose();
        }
    }
}
