using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Windsor
{

    public class MsLifetimeScopeProvider
    {

        public MsLifetimeScope LifetimeScope { get; }


        public MsLifetimeScopeProvider(GlobalMsLifetimeScopeProvider globalMsLifetimeScopeProvider)
        {
            LifetimeScope = MsLifetimeScope.Current ??
                            globalMsLifetimeScopeProvider.LifetimeScope;
        }
    }
}
