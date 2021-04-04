using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Localization 
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTBBProjectInternationalization(this IServiceCollection services)
        {
            services.AddSingleton<IConfigureOptions<MvcOptions>, MvcDataAnnotationOptionSetup>();
            services.AddSingleton<IStringLocalizerFactory, DatabaseStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, DatabaseStringLocalizer>();
            services.AddSingleton<IResourceService, ResourceService>();
            services.AddMvc().AddDataAnnotationsLocalization();
        }

        public static void UseTBBProjectInternationalization(this IApplicationBuilder app)
        {
            var supportedCultures = new List<CultureInfo>();
            RequestCulture defaultCulture = null;

            // !!!!! TASK #777;
            /*
            var unitOfWork = app.ApplicationServices.CreateUnitOfWork();
            var languageRepo = unitOfWork.Repository<Language>();var languages = languageRepo.TableNoTracking.AsEnumerable();

            foreach (var language in languages)
            {
                if (defaultCulture == null && language.IsDefault)
                {
                    defaultCulture = new RequestCulture(language.CultureName);
                }

                supportedCultures.Add(new CultureInfo(language.CultureName));
            }*/

            defaultCulture = new RequestCulture("tr-TR");
            supportedCultures.Add(new CultureInfo("tr-TR"));

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = defaultCulture,
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(requestLocalizationOptions);
        }
    }
}