namespace TBBProject.Core.Common.Web
{
    using Microsoft.AspNetCore.Builder;
    public static class SecurityApplicationBuilderExtension
    {
        /// <summary>
        /// Uses Default Cors Policy that Allows Any Header, Any Origin and Any method.
        /// </summary>
        /// <param name="app">App</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseDefaultCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors("TBBProjectDefaultCorsPolicy");
            return app;
        }

        /// <summary>
        /// Uses HSTS. Configures HSTS for one day only, including subdomain.s
        /// CAUTION: This is irreversible once done.
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseTBBProjectHsts(this IApplicationBuilder app)
        {
            app.UseHsts(options => options.MaxAge(7).IncludeSubdomains());
            return app;
        }

        /// <summary>
        /// Uses Https Redirection. Redirects HTTP requests over to HTTPS
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseTBBProjectHttpsRedirection(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            return app;
        }

        /// <summary>
        /// Uses X-XSS protection with Block mode.
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseXXssProtectionWithBlockedMode(this IApplicationBuilder app)
        {
            app.UseXXssProtection(options =>
            {
                options.Enabled();
            });
            return app;
        }

        /// <summary>
        /// Upgrades insecure requests.
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseUpgradeInsecureRequests(this IApplicationBuilder app)
        {
            app.UseCsp(options => options.UpgradeInsecureRequests());
            return app;
        }


        /// <summary>
        /// No Referrer is allowed
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseReferrerPolicies(this IApplicationBuilder app)
        {
            app.UseReferrerPolicy(options => options.NoReferrer() );
            return app;
        }

        /// <summary>
        /// Uses redirect validation.
        /// Redirect to any authority should be configured here. 
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseTBBProjectRedirectValidation(this IApplicationBuilder app)
        {
            app.UseRedirectValidation(options =>
            {
                options.AllowSameHostRedirectsToHttps();
            });
            return app;
        }

        /// <summary>
        /// X-Frame-Options HTTP header prevents click-jacking by stopping the page to be embedded as IFrame.
        /// There are 3 options: SameOrigin, Deny, Disabled.
        /// </summary>
        /// <param name="app">app</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UsePerzufuntXFrameOptions(this IApplicationBuilder app)
        {
            app.UseXfo(options => options.SameOrigin());
            return app;
        }
    }
}
