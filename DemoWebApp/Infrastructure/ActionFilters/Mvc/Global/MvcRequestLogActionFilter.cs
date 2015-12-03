using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Serilog;

namespace DemoWebApp.Infrastructure.ActionFilters.Mvc.Global
{
    public class MvcRequestLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        private readonly Stopwatch _sw;

        public MvcRequestLogActionFilter(ILogger logger)
        {
            _logger = logger;
            _sw = Stopwatch.StartNew();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var logger = _logger;
            var requestHeaders = filterContext.HttpContext.Request.Headers;
            foreach (var key in requestHeaders.AllKeys)
            {
                if (key == "Authorization") continue;
                var value = requestHeaders[key];
                logger = logger.ForContext(key, value);
            }

            _logger.Debug("HTTP {HttpMethod} to {RawUrl} ({@RequestHeaders}) {RequestState}",
                filterContext.HttpContext.Request.HttpMethod,
                filterContext.HttpContext.Request.Url.AbsolutePath,
                requestHeaders,
                "started");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            _sw.Stop();

            var exception = filterContext.Exception;
            if (exception == null)
            {
                _logger.Information("HTTP {HttpMethod} ({RequestDuration}) to {RawUrl} {RequestState}",
                    filterContext.HttpContext.Request.HttpMethod,
                    _sw.Elapsed,
                    filterContext.HttpContext.Request.Url.AbsolutePath,
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
                    filterContext.HttpContext.Request.HttpMethod,
                    _sw.Elapsed,
                    filterContext.HttpContext.Request.Url.AbsolutePath,
                    "failed",
                    exception.Message);
            }
        }
    }
}