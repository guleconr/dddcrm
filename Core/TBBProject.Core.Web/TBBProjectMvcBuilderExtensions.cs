using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TBBProject.Core.Web
{
    public static class TBBProjectMvcBuilderExtensions
    {
        public static IMvcBuilder Configure(this IMvcBuilder mvcBuilder, IServiceCollection services)
        {
            mvcBuilder.AddMvcOptions(options =>
            {
                var cacheProfiles = new ResponseCacheProvider().GetCacheProfiles();
                foreach (var item in cacheProfiles)
                {
                    options.CacheProfiles.Add(item);
                }

                options.UseHtmlEncodeModelBinding();
            });

            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Latest);
            //mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            mvcBuilder.AddXmlSerializerFormatters();
            mvcBuilder.AddXmlDataContractSerializerFormatters();
            mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }

        public static IMvcBuilder AddControllersAsServices(this IMvcBuilder builder)
        {
            var feature = new ControllerFeature();
            builder.PartManager.PopulateFeature(feature);

            var currentComponentController = feature.Controllers
                .Where(c => c.AsType().Assembly.FullName == typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly.FullName)
                .Select(c => c.AsType());

            foreach (var controller in currentComponentController)
            {
                builder.Services.AddTransient(controller, controller);
            }

            return builder;
        }
    }
}
