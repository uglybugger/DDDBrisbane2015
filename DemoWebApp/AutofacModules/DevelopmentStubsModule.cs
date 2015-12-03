using Autofac;
using DemoWebApp.Core.DevelopmentStubs;
using DemoWebApp.Core.Infrastructure;

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

            builder.RegisterType<FakeDbContext>()
                .As<IFakeDbContext>()
                .SingleInstance();

            builder.RegisterGeneric(typeof (MemoryRepository<>))
                .As(typeof (IRepository<>))
                .SingleInstance();
        }
    }
}