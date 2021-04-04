using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    [DebuggerStepThrough]
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-ID";
        private readonly ILogger<CorrelationIdMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate requestDelegate, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdAccessor correlationIdAccessor)
        {
            var correlationId = correlationIdAccessor.GetCorrelationId();
            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            correlationIdAccessor.SetCorrelationId(correlationId);

            await _next(context);

            if (!correlationId.Equals(correlationIdAccessor.GetCorrelationId()))
            {
                throw new Exception("Correlation ID not equal");
            }
        }
    }
}
