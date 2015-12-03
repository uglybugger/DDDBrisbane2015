using Autofac;
using DemoWebApp.Core;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.AutofacModules
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof (MemoryRepository<>))
                .As(typeof (IRepository<>))
                .SingleInstance();
        }
    }
}