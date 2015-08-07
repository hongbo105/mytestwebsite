using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using MyFixIt.Logging;
using MyFixIt.Persistence;
using Autofac;
using System.Threading.Tasks;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private ILogger logger;
        private IContainer container;
        private CancellationTokenSource token = new CancellationTokenSource();
        public override void Run()
        {
            // var logger = new Logger();
            // This is a sample worker implementation. Replace with your logic.
            
            logger.Information("WorkerRole1 entry point called", "Information");

            var task = RunAsync(token.Token);

            try
            {
                task.Wait();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to run iFixTask");
            }
        }

        private Task RunAsync(CancellationToken token)
        {
            using (var scope = container.BeginLifetimeScope)
            {

            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            var container = builder.Build();

            logger = container.Resolve<ILogger>();

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
