using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DemoWebApp.Infrastructure.ActionFilters.WebApi.Global;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using TypeExtensions = ThirdDrawer.Extensions.TypeExtensionMethods.TypeExtensions;

namespace DemoWebApp.AutofacModules
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterApiControllers(ThisAssembly);
            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            ThisAssembly
                .DefinedTypes
                .Where(t => t.IsAssignableTo<IAutofacActionFilter>())
                .Where(TypeExtensions.IsInstantiable)
                .Where(t => t.IsInNamespaceOf<WebApiRequestLogActionFilter>())
                .Do(t => builder.RegisterType(t)
                    .AsWebApiActionFilterFor<ApiController>()
                    .InstancePerLifetimeScope())
                .Done();

            ThisAssembly
                .DefinedTypes
                .Where(t => t.IsAssignableTo<IAutofacActionFilter>())
                .Where(TypeExtensions.IsInstantiable)
                .Where(t => t.IsInNamespaceOf<WebApiRequestLogActionFilter>())
                .Do(t => builder.RegisterType(t)
                    .AsWebApiActionFilterFor<ApiController>()
                    .InstancePerLifetimeScope())
                .Done();
        }
    }
}