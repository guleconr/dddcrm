using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TBBProject.Core.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditAttribute : Attribute
    {
    }

    [DebuggerStepThrough]
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuditHelper _auditHelper;

        public AuditActionFilter(IAuditHelper auditHelper) => _auditHelper = auditHelper;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var methodInfo = context.ActionDescriptor.AsControllerActionDescriptor().MethodInfo;
            var controllerInfo = context.ActionDescriptor.AsControllerActionDescriptor().ControllerTypeInfo.AsType();
            var actionArguments = context.ActionArguments;

            var auditInfo = _auditHelper.CreateAudit(controllerInfo, methodInfo, actionArguments);

            if (!_auditHelper.ShouldSaveAudit(methodInfo))
            {
                await next();
                return;
            }

            try
            {
                var result = await next();

                if (result.Exception != null && !result.ExceptionHandled)
                {
                    auditInfo.Exception = result.Exception;
                }
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                await _auditHelper.SaveAsync(auditInfo);
            }
        }
    }
}
