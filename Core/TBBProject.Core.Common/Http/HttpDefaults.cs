namespace TBBProject.Core.Common
{
    public class HttpDefaults
    {
        /// <summary>
        /// Gets the name of HTTP_X_FORWARDED_PROTO header
        /// </summary>
        public static string HttpXForwardedProtoHeader => "X-Forwarded-Proto";

        /// <summary>
        /// Gets the name of X-FORWARDED-FOR header
        /// </summary>
        public static string XForwardedForHeader => "X-FORWARDED-FOR";
    }
}
