using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Serilog;

namespace DemoWebApp
{
    public class WebApiApplication : HttpApplication
    {
        private IContainer _container;

        protected void Application_Start()
        {
            _container = IoC.LetThereBeIoC();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Services.Replace(typeof (IExceptionLogger), new SerilogExceptionLogger());
        }

        protected void Application_End()
        {
            var container = _container;
            if (container != null) container.Dispose();
        }
    }

    public class SerilogExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //var logContext= Log.Logger;
                //foreach (var key in context.Exception.Data.Keys){
                //    logContext = logContext.ForContext(key, context.Exception.Data[key]);
                //}
                //var c = Log.Logger.ForContext("foo", "bar").;
                Log.Error(context.Exception, context.Exception.Message);
            });
        }
    }
}