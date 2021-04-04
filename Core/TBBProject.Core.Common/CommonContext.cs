using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;

namespace TBBProject.Core.Common
{
    public class CommonContext : ICommonContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICorrelationIdAccessor _correlationIdAccessor;
        private string _sessionId;

        public CommonContext(IHttpContextAccessor httpContextAccessor, ICorrelationIdAccessor correlationIdAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._correlationIdAccessor = correlationIdAccessor;
        }

        public string AssemblyName => Assembly.GetEntryAssembly().GetName().Name;

        public string WorkingLanguage => Thread.CurrentThread.CurrentUICulture.Name;

        public string UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext.User.Identity?.Name;
                return userId;
            }
        }

        public string CorrelationId => _correlationIdAccessor.GetCorrelationId();

        public string RequesterIpAddress => GetRequesterIpAddress();

        public string SessionId
        {
            get
            {
                if (!this._sessionId.IsNullOrEmpty())
                {
                    return this._sessionId;
                }

                var sessionFeauture = _httpContextAccessor.HttpContext.Features.Get<ISessionFeature>();

                if (sessionFeauture == null)
                {
                    _sessionId = string.Empty;
                    return _sessionId;
                }

                else if (_httpContextAccessor.HttpContext.Session != null && _httpContextAccessor.HttpContext.Session.IsAvailable)
                {
                    _sessionId = _httpContextAccessor.HttpContext.Session.Id;

                    return _sessionId;
                }

                else
                {
                    _sessionId = SessionId;
                    return _sessionId;
                }

            }

            set => _sessionId = value;

        }

        public string MachineIp => Environment.MachineName;
        
        public string GetRequesterIpAddress()
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

        public bool IsRequestAvailable()
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
    }

}
