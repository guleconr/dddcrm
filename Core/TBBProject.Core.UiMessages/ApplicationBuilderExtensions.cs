using Microsoft.AspNetCore.Builder;

namespace TBBProject.Core.UiMessages
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAjaxMessages(this IApplicationBuilder application)
        {
            application.UseMiddleware<AjaxMessagesMiddleware>();

            return application;
        }
    }
}
