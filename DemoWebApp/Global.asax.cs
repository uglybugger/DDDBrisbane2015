using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DemoWebApp.Infrastructure.Logging;

namespace DemoWebApp
{
    public class WebApiApplication : HttpApplication
    {
        private IContainer _container;

        protected void Application_Start()
        {
            _container = IoC.LetThereBeIoC();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Services.Replace(typeof (IExceptionLogger), new SerilogExceptionLogger());
        }

        protected void Application_End()
        {
            var container = _container;
            if (container != null) container.Dispose();
        }
    }
}