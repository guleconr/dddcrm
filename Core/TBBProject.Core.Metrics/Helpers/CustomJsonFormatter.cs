using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Buffers;

namespace TBBProject.Core.Metrics
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomJsonFormatter : ActionFilterAttribute
    {
        private readonly string formatName = string.Empty;
        public CustomJsonFormatter(string _formatName) => this.formatName = _formatName;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context == null || context.Result == null)
            {
                return;
            }

            var settings = JsonSerializerSettingsProvider.CreateSerializerSettings();

            if (this.formatName == "camel")
            {
                settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            }
            else
            {
                settings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            }

            var formatter = new JsonOutputFormatter(settings, ArrayPool<char>.Shared);

            (context.Result as Microsoft.AspNetCore.Mvc.OkObjectResult).Formatters.Add(formatter);
        }
    }
}
