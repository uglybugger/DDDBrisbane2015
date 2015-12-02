using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace DemoWebApp.AutofacModules
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterApiControllers(ThisAssembly);
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
        }
    }
}