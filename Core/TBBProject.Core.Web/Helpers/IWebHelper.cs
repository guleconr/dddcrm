namespace TBBProject.Core.Web
{
    using Microsoft.AspNetCore.Http;

    public interface IWebHelper
    {
        bool IsRequestBeingRedirected { get; }

        bool IsPostBeingDone { get; set; }

        string CurrentRequestProtocol { get; }

        string GetUrlReferrer();

        string GetRequesterIpAddress();

        string GetThisPageUrl(bool includeQueryString, bool? useSsl = null, bool lowercaseUrl = false);

        bool IsCurrentConnectionSecured();

        bool IsStaticResource();

        bool IsLocalRequest(HttpRequest req);

        string GetRawUrl(HttpRequest request);
    }
}
