using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Windsor
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Castle.Windsor;

    /// <summary>
    ///     Factory per il provider dei servizi di windsor
    /// </summary>
    public class WindsorServiceProviderFactory : IServiceProviderFactory<IWindsorContainer>
    {
        public IWindsorContainer CreateBuilder(IServiceCollection services)
        {
            var container = services.GetSingletonServiceOrNull<IWindsorContainer>();

            if (container == null)
            {
                container = new WindsorContainer();
                services.AddSingleton(container);
            }

            WindsorRegistrationHelper.AddServices(container, services);

            return container;
        }

        public IServiceProvider CreateServiceProvider(IWindsorContainer containerBuilder)
        {
            return containerBuilder.Resolve<IServiceProvider>();
        }
    }
}
