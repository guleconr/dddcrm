using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.Web
{
    [DebuggerStepThrough]
    public class UserIdCookieMiddleware
    {
        private RequestDelegate _next;
        private const string Uid = "uid";
        public UserIdCookieMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated == false)
            {
                if (context.Request.Cookies.ContainsKey(Uid) == false)
                {
                    var userId = Guid.NewGuid().ToString();
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddYears(3),
                        Path = "/",
                        Secure = true,
                        IsEssential = true
                    };
                    context.Response.Cookies.Append(Uid, userId, cookieOptions);
                }
            }
            await _next.Invoke(context);
        }
    }
}
