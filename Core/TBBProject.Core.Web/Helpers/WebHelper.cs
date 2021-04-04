namespace TBBProject.Core.Web
{
    using System;
    using System.Linq;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Net.Http.Headers;
    using TBBProject.Core.Common;

    public class WebHelper : IWebHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return false;
            }

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        protected virtual bool IsIpAddressSet(IPAddress address)
        {
            return address != null && address.ToString() != IPAddress.IPv6Loopback.ToString();
        }

        public virtual string GetUrlReferrer()
        {
            if (!IsRequestAvailable())
            {
                return string.Empty;
            }

            return _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer];
        }

        public virtual string GetRequesterIpAddress()
        {
            if (!IsRequestAvailable())
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    var forwardedHttpHeaderKey = HttpDefaults.XForwardedForHeader;

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                    {
                        result = forwardedHeader.FirstOrDefault();
                    }
                }

                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                {
                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }

            if (result != null && result.Equals(IPAddress.IPv6Loopback.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                result = IPAddress.Loopback.ToString();
            }

            if (IPAddress.TryParse(result ?? string.Empty, out var ip))
            {
                result = ip.ToString();
            }
            else if (!string.IsNullOrEmpty(result))
            {
                result = result.Split(':').FirstOrDefault();
            }

            return result;
        }

        public virtual string GetThisPageUrl(bool includeQueryString, bool? useSsl = null, bool lowercaseUrl = false)
        {
            if (!IsRequestAvailable())
            {
                return string.Empty;
            }

            var pageUrl = $"{_httpContextAccessor.HttpContext.Request.Path}";

            if (includeQueryString)
            {
                pageUrl = $"{pageUrl}{_httpContextAccessor.HttpContext.Request.QueryString}";
            }

            if (lowercaseUrl)
            {
                pageUrl = pageUrl.ToLowerInvariant();
            }

            return pageUrl;
        }

        public virtual bool IsCurrentConnectionSecured()
        {
            if (!IsRequestAvailable())
            {
                return false;
            }

            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        public virtual string GetHost(bool useSsl)
        {
            if (!IsRequestAvailable())
            {
                return string.Empty;
            }

            var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
            if (StringValues.IsNullOrEmpty(hostHeader))
            {
                return string.Empty;
            }

            var storeHost = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}{Uri.SchemeDelimiter}{hostHeader.FirstOrDefault()}";

            storeHost = $"{storeHost.TrimEnd('/')}/";

            return storeHost;
        }

        public virtual bool IsStaticResource()
        {
            if (!IsRequestAvailable())
            {
                return false;
            }

            string path = _httpContextAccessor.HttpContext.Request.Path;

            var contentTypeProvider = new FileExtensionContentTypeProvider();
            return contentTypeProvider.TryGetContentType(path, out string _);
        }

        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContextAccessor.HttpContext.Response;
                int[] redirectionStatusCodes = { StatusCodes.Status301MovedPermanently, StatusCodes.Status302Found };
                return redirectionStatusCodes.Contains(response.StatusCode);
            }
        }

        public virtual string CurrentRequestProtocol => IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

        public bool IsPostBeingDone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual bool IsLocalRequest(HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (IsIpAddressSet(connection.RemoteIpAddress))
            {
                return IsIpAddressSet(connection.LocalIpAddress)
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        public virtual string GetRawUrl(HttpRequest request)
        {
            var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

            if (string.IsNullOrEmpty(rawUrl))
            {
                rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";
            }

            return rawUrl;
        }
    }
}