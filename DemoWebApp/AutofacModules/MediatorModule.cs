using Autofac;
using DemoWebApp.Core.Mediation;
using DemoWebApp.Infrastructure;

namespace DemoWebApp.AutofacModules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assemblies = new[] {ThisAssembly, typeof (IMediator).Assembly};

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof (IHandleRequest<,>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof (IHandleEvent<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof (IHandleCommand<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<AutofacMediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
        }
    }
}