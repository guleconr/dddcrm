using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.UiMessages
{
    [DebuggerStepThrough]
    internal class AjaxMessagesMiddleware
    {
        private IMessage _message;
        private const string AccessControlExposeHeadersKey = "Access-Control-Expose-Headers";
        private readonly RequestDelegate _next;

        public AjaxMessagesMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, IMessage message)
        {
            _message = message;
            context.Response.OnStarting(Callback, context);
            await _next(context);
        }

        private Task Callback(object context)
        {
            var httpContext = (HttpContext)context;
            if (httpContext.Request.IsAjaxRequest())
            {
                /*
                var messages = _message.Get();
                if (messages != null && messages.Any())
                {
                    var accessControlExposeHeaders = $"{GetControlExposeHeaders(httpContext.Response.Headers)}";
                    httpContext.Response.Headers.Add(AccessControlExposeHeadersKey, accessControlExposeHeaders);

                    var messagesJson = messages.ToJson();
                    httpContext.Response.Headers.Add(Constants.ResponseHeaderKey, messagesJson);
                }
                */
            }
            return Task.FromResult(0);
        }

        private object GetControlExposeHeaders(IHeaderDictionary headers)
        {
            var existingHeaders = headers[AccessControlExposeHeadersKey];
            if (string.IsNullOrEmpty(existingHeaders))
            {
                return Constants.ResponseHeaderKey;
            }
            else
            {
                return $"{existingHeaders}, {Constants.ResponseHeaderKey}";
            }
        }
    }
}
