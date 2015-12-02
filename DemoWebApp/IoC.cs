using Autofac;

namespace DemoWebApp
{
    public static class IoC
    {
        public static IContainer LetThereBeIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof (WebApiApplication).Assembly);
            return builder.Build();
        }
    }
}