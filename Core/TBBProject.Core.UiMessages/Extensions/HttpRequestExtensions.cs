using Microsoft.AspNetCore.Http;
using System;

namespace TBBProject.Core.UiMessages
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return true;
            }
            if (request.Headers != null)
            {
                var isIt = request.Headers["X-Requested-With"] == "XMLHttpRequest";

                return isIt;
            }
            return false;
        }
    }
}
