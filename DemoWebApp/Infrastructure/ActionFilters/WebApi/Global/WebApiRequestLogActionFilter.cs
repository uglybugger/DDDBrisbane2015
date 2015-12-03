using System.Diagnostics;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Serilog;

namespace DemoWebApp.Infrastructure.ActionFilters.WebApi.Global
{
    public class WebApiRequestLogActionFilter : IAutofacActionFilter
    {
        private readonly ILogger _logger;

        private readonly Stopwatch _sw;

        public WebApiRequestLogActionFilter(ILogger logger)
        {
            _logger = logger;
            _sw = Stopwatch.StartNew();
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            var logger = _logger;
            var requestHeaders = actionContext.Request.Headers;
            foreach (var kvp in requestHeaders.AsQueryable())
            {
                if (kvp.Key == "Authorization") continue;
                logger = logger.ForContext(kvp.Key, kvp.Value);
            }

            var httpMethod = actionContext.Request.Method;
            var requestUrl = actionContext.Request.RequestUri.AbsolutePath;

            _logger.Debug("HTTP {HttpMethod} to {RawUrl} ({@RequestHeaders}) {RequestState}",
                httpMethod,
                requestUrl,
                requestHeaders,
                "started");
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _sw.Stop();

            var exception = actionExecutedContext.Exception;
            var httpMethod = actionExecutedContext.Request.Method;
            var requestUrl = actionExecutedContext.Request.RequestUri.AbsolutePath;

            if (exception == null)
            {
                _logger.Information("HTTP {HttpMethod} ({RequestDuration}) to {RawUrl} {RequestState}",
                    httpMethod,
                    _sw.Elapsed,
                    requestUrl,
                    "completed");
            }
            else
            {
                var logContext = _logger;
                foreach (var key in exception.Data.Keys.OfType<string>())
                {
                    logContext = logContext.ForContext(key, exception.Data[key]);
                }

                logContext.Error(exception, "HTTP {HttpMethod} ({RequestDuration}) to {RawUrl} {RequestState} ({Message})",
                    httpMethod,
                    _sw.Elapsed,
                    requestUrl,
                    "failed",
                    exception.Message);
            }
        }
    }
}