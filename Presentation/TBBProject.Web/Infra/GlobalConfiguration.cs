namespace TBBProject.Core.Web
{
    public static class GlobalConfiguration
    {
        public static string DefaultCulture => "en-US";

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }
        public static int PageSize { get; set; }
    }

    public class ApplicationSettings
    {
        public string PageSize { get; set; }
    }
}
