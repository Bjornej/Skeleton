using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using System;
using System.Linq;
using System.Reflection;

namespace Web
{
    public class NServiceBusConfig
    {

        public static void ConfigureBus(IWindsorContainer container, IConfiguration config)
        {
            var queue = config["NServiceBus:Endpoint"];
            var cfg = new EndpointConfiguration(queue);

            cfg.Recoverability()
                .Immediate(retry => retry.NumberOfRetries(2))
                .Delayed(delayed => delayed.NumberOfRetries(2));

            var errorQueueName = queue + ".error";
            cfg.SendFailedMessagesTo(errorQueueName);

            //cfg.License(Resource.Licenza); TODO passare la licenza
            cfg.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));
            cfg.Conventions()
                 .DefiningCommandsAs(t => !t.IsAbstract && !t.IsInterface && typeof(Framework.ICommand).IsAssignableFrom(t))
                 .DefiningEventsAs(t => !t.IsAbstract && !t.IsInterface && typeof(Framework.IEvent).IsAssignableFrom(t));
            cfg.EnableInstallers();
            cfg.UsePersistence<InMemoryPersistence, StorageType.Timeouts>();
            cfg.UnitOfWork().WrapHandlersInATransactionScope();
            cfg.AssemblyScanner().ExcludeAssemblies("netstandard", "dotnet-bundle.exe");

            cfg.UsePersistence<MsmqPersistence>()
                .SubscriptionQueue(queue + ".subscriptions");
            var transport = cfg.UseTransport<MsmqTransport>()
                .Transactions(TransportTransactionMode.SendsAtomicWithReceive);

            var routing = transport.Routing();
            var subscriptions = config.GetSection("NServiceBus:Subscriptions")
                                     .GetChildren().ToList();

            foreach (var sub in subscriptions)
            {
                var ass = Assembly.Load(sub["Assembly"]);
                routing.RouteToEndpoint(
                    assembly: ass,
                    destination: sub["Queue"]);

                if (ass.ExportedTypes.Any(t => !t.IsAbstract && !t.IsInterface && typeof(Framework.IEvent).IsAssignableFrom(t)))
                {
                    routing.RegisterPublisher(
                        assembly: ass,
                        publisherEndpoint: sub["Queue"]);
                }
            }



            cfg.UseSerialization<NewtonsoftSerializer>();
            cfg.PurgeOnStartup(false);
            cfg.DefineCriticalErrorAction(async (context) =>
            {
                try
                {
                    await context.Stop().ConfigureAwait(false);

                }
                finally
                {

                    Environment.FailFast("Critical error shutting down: " + context.Error + ".", context.Exception);

                }
            });

            var busInstance = Endpoint.Start(cfg).GetAwaiter().GetResult();
            container.Register(Component.For<IEndpointInstance>().Instance(busInstance).LifestyleSingleton());
        }
    }
}
