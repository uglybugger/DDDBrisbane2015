using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Serilog;

namespace DemoWebApp.Infrastructure.Logging
{
    public class SerilogExceptionLogger : IExceptionLogger
    {
        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return Task.Run(() => { Log.Error(context.Exception, context.Exception.Message); }, cancellationToken);
        }
    }
}