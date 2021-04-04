using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Web
{
    public interface IAuditHelper
    {
        Audit CreateAudit(Type type, MethodInfo method, object[] arguments);
        Audit CreateAudit(Type type, MethodInfo method, IDictionary<string, object> arguments);
        void Save(Audit audit);
        Task SaveAsync(Audit audit);
        bool ShouldSaveAudit(MethodBase methodBase);
    }

    public class AuditHelper : IAuditHelper
    {
        private readonly ILogger<AuditHelper> _logger;
        private readonly IAuditStore _auditStore;
        private readonly IJsonSerializer _serializer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHelper _webHelper;

        public AuditHelper(ILogger<AuditHelper> logger,
            IAuditStore auditStore,
            IJsonSerializer serializer,
            IHttpContextAccessor httpContextAccessor,
            IWebHelper webHelper)
        {
            _logger = logger;
            _auditStore = auditStore;
            _serializer = serializer;
            _httpContextAccessor = httpContextAccessor;
            _webHelper = webHelper;
        }

        public Audit CreateAudit(Type type, MethodInfo method, object[] arguments)
        {
            return CreateAudit(type, method, CreateArgumentsDictionary(method, arguments));
        }

        public Audit CreateAudit(Type type, MethodInfo method, IDictionary<string, object> arguments)
        {
            var audit = new Audit()
            {
                DateTime = DateTime.Now.ToLocalTime(),
                Args = ConvertArgumentsToJson(arguments),
                ClassName = type != null ? type.FullName : "",
                MethodName = method.Name,
                User = _httpContextAccessor.HttpContext.User.Identity.Name,
                UserIp = _webHelper.GetRequesterIpAddress()
            };

            return audit;
        }
        private static Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[parameters[i].Name] = arguments[i];
            }

            return dictionary;
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    dictionary[argument.Key] = argument.Value;
                }

                return _serializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), ex);
                return "{}";
            }
        }

        public void Save(Audit audit)
        {
            Ensure.That("audit", audit).Is.NotNull();

            _auditStore.Save(audit);
        }

        public async Task SaveAsync(Audit audit)
        {
            Ensure.That("audit", audit).Is.NotNull();

            await _auditStore.SaveAsync(audit);
        }

        public bool ShouldSaveAudit(MethodBase methodBase)
        {
            if (methodBase == null)
            {
                return false;
            }

            if (!methodBase.IsPublic)
            {
                return false;
            }

            if (methodBase.IsDefined(typeof(DisableAuditAttribute), true))
            {
                return false;
            }

            var callingClass = methodBase.DeclaringType;

            if (callingClass != null)
            {
                if (callingClass.GetTypeInfo().IsDefined(typeof(DisableAuditAttribute), true))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
