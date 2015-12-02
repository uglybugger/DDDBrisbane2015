using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DemoWebApp.ActionFilters.Global;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using TypeExtensions = ThirdDrawer.Extensions.TypeExtensionMethods.TypeExtensions;

namespace DemoWebApp.AutofacModules
{
    public class MvcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(ThisAssembly);
            builder.RegisterFilterProvider();
            ThisAssembly
                .DefinedTypes
                .Where(t => t.IsAssignableTo<IActionFilter>())
                .Where(TypeExtensions.IsInstantiable)
                .Where(t => t.IsInNamespaceOf<RequestLogActionFilter>())
                .Do(t => builder.RegisterType(t)
                    .AsActionFilterFor<Controller>()
                    .InstancePerLifetimeScope())
                .Done();
        }
    }
}