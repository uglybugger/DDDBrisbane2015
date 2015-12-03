using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DemoWebApp.Infrastructure;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using ThirdDrawer.Extensions.StringExtensionMethods;

namespace DemoWebApp.api.v4
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var validationErrors = actionContext.ActionArguments.Values
                .SelectMany(arg => arg.ValidationErrors())
                .ToArray();

            if (validationErrors.None()) return;

            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, validationErrors.Join(Environment.NewLine));
        }
    }
}