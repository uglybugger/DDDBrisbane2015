using System.Web.Mvc;
using DemoWebApp.Core.Infrastructure;

namespace DemoWebApp.Infrastructure.ActionFilters.Mvc.Global
{
    public class MvcUnitOfWorkActionFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public MvcUnitOfWorkActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (filterContext.Exception == null)
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