using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Infrastructure.ActionFilters.WebApi.Global
{
    public class WebApiUnitOfWorkActionFilter : IAutofacActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebApiUnitOfWorkActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                _unitOfWork.Complete();
            }
            else
            {
                _unitOfWork.Abandon();
            }
        }
    }
}