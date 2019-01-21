using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Web
{

    public class HostingConfig
    {

        private IWebHost host { get; set; }

        private static IWindsorContainer container { get; set; }

        public static IWindsorContainer GetContainer()
        {
            return container;
        }

        /// <summary>
        ///     Metodo eseguito alla partenza del servizio, avvia la configurazione di NServiceBus
        /// </summary>
        public void Start(string[] args)
        {

            var dir = Directory.GetCurrentDirectory();
            if (dir == Environment.SystemDirectory)
            {
                dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            }

            container = new WindsorContainer();


            using (container.BeginScope())
            {
                host = WebHost.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration((builderContext, config) =>
                     {
                         config.AddJsonFile("appsettings.json", optional: false);
                     })
                              .UseStartup<Startup>()
                              .UseContentRoot(dir)
                              .Build();

                NServiceBusConfig.ConfigureBus(container, (IConfiguration)host.Services.GetService(typeof(IConfiguration)));


                host.Run();
            }
        }

        /// <summary>
        ///     Metodo eseguito allo stop del servizio
        /// </summary>
        public void Stop()
        {

            ((IApplicationLifetime)host.Services.GetService(typeof(IApplicationLifetime))).StopApplication();
            host.Dispose();

        }
    }
}