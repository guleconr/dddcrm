using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace TBBProject.Core.Common
{
    [ExcludeFromCodeCoverage]
    public class CorrelationIdAccessor : ICorrelationIdAccessor
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CorrelationIdAccessor> _logger;

        public CorrelationIdAccessor(IHttpContextAccessor httpContextAccessor, ILogger<CorrelationIdAccessor> logger)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._logger = logger;
        }

        public string GetCorrelationId()
        {
            if (this._httpContextAccessor.HttpContext == null)
            {
                this._logger.LogWarning("HttpContext is null while trying to get correlation id.");
                return string.Empty;
            }

            this._httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId);

            if(string.IsNullOrEmpty(correlationId))
            {
                correlationId = this._httpContextAccessor.HttpContext.Request.Query["correlationid"].ToString();
            }

            return correlationId;
        }

        public void SetCorrelationId(string correlationId)
        {
            if (this._httpContextAccessor.HttpContext.Request.Headers.ContainsKey(CorrelationIdHeaderName) == false)
            {
                this._httpContextAccessor.HttpContext.Request.Headers.Add(CorrelationIdHeaderName, correlationId);
            }
            else
            {
                this._httpContextAccessor.HttpContext.Request.Headers[CorrelationIdHeaderName] = correlationId;
            }

            if (this._httpContextAccessor.HttpContext.Response.Headers.ContainsKey(CorrelationIdHeaderName) == false)
            {
                this._httpContextAccessor.HttpContext.Response.Headers.Add(CorrelationIdHeaderName, correlationId);
            }
            else
            {
                this._httpContextAccessor.HttpContext.Response.Headers[CorrelationIdHeaderName] = correlationId;
            }
        }
    }
}
