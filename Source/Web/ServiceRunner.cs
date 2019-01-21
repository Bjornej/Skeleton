using System;
using System.Diagnostics;
using System.IO;
using Topshelf;

namespace Web
{
    public static class ServiceRunner
    {
        public static void Start(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Unahndled exception Appdomain: " + e.ExceptionObject.ToString(), EventLogEntryType.Error, 101, 1);
                }
            };


            HostFactory.Run(x =>
            {
                x.Service<HostingConfig>(s =>
                {
                    s.ConstructUsing(name => new HostingConfig());
                    s.WhenStarted(tc => tc.Start(args));
                    s.WhenStopped(tc => tc.Stop());
                });
            });


        }
    }
}
