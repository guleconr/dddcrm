namespace TBBProject.Core.Web
{
    public class ResponseCacheOptions
    {
        public int SizeLimit { get; set; } //Default 100MB
        public bool UseCaseSensitivePaths { get; set; }
        public long MaximumBody { get; set; } // Default 64MB
    }
}
