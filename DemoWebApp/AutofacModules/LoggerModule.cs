using Autofac;
using AutofacSerilogIntegration;
using Serilog;
using SerilogWeb.Classic;

namespace DemoWebApp.AutofacModules
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq("http://localhost:5341")
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .CreateLogger();

            Log.Logger = logger;
            ApplicationLifecycleModule.IsEnabled = true;
            ApplicationLifecycleModule.LogPostedFormData = LogPostedFormDataOption.OnlyOnError; 

            Log.Information("Logger online");

            builder.RegisterLogger();
        }
    }
}