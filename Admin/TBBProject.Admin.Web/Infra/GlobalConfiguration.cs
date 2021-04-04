namespace TBBProject.Admin.Web
{
    public static class GlobalConfiguration
    {
        public static string DefaultCulture => "tr-TR";

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }
        public static int PageSize { get; set; }
    }

    public class ApplicationSettings
    {
        public string PageSize { get; set; }
    }
}
