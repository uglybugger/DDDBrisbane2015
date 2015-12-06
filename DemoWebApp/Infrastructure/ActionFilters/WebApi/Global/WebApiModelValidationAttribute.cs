using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Serilog;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace DemoWebApp.Infrastructure.ActionFilters.WebApi.Global
{
    public class WebApiModelValidationAttribute : IAutofacActionFilter
    {
        private readonly ILogger _logger;

        public WebApiModelValidationAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            var validationErrors = actionContext.ActionArguments.Values
                .NotNull()
                .SelectMany(arg => arg.ValidationErrors())
                .ToArray();

            if (validationErrors.None()) return;

            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, validationErrors.Join(Environment.NewLine));
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null) return;

            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent == null) return;

            var dto = objectContent.Value;
            var validationErrors = dto.ValidationErrors().ToArray();
            if (validationErrors.None()) return;

            _logger.Error("A controller action returned an invalid response DTO: {@ValidationErrors} {@Dto}", validationErrors, dto);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, validationErrors.Join(Environment.NewLine));
        }
    }
}