using System;
using Microsoft.AspNetCore.Mvc.Filters;
using TBBProject.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TBBProject.Web.Extensions
{
    public class UrlLocalizationFilter : IActionFilter
    {
        private readonly IUnitOfWork _uow;
        //private readonly IRepository<ProcessLogs> _processLogsRepository;

        public UrlLocalizationFilter(IUnitOfWork uow)
        {
            _uow = uow;
           // _processLogsRepository = uow.Repository<ProcessLogs>();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            var queryPath = context.HttpContext.Request.Path.ToString();
            var queryStringId = context.HttpContext.Request.Query["culture"].ToString();
            if(queryPath=="/")
            {
                context.Result = new RedirectResult("/Tr/Home/Index");
                return;
            }
            
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
    public class AsyncActionFilterExample : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
        }
    }
}
