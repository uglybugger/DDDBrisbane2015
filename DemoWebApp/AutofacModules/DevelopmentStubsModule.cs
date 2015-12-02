using Autofac;

namespace DemoWebApp.AutofacModules
{
    public class DevelopmentStubsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IStartable>()
                .As<IStartable>()
                .SingleInstance();
        }
    }
}