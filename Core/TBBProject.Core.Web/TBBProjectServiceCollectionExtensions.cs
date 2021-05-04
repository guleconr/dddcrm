using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using TBBProject.Core.Common;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using TBBProject.Core.Localization;

namespace TBBProject.Core.Web
{
    public static class TBBProjectServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IUserAgentHelper, UserAgentHelper>();
            var manager = TBBProjectContext.Create();
            manager.Initialize(services);
            services.AddSingleton<IFileSystemFileProvider, FileSystemProvider>();
            var serviceProvider = manager.ConfigureServices(services, configuration);
            var assembly = typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly;
            var part = new AssemblyPart(assembly);
            services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();
            return serviceProvider;
        }

        public static void AddHttpSession(this IServiceCollection services) => services.AddSession(options =>
                                                                             {
                                                                                 options.Cookie.Name = $"TBBProject.Session.";
                                                                                 options.Cookie.HttpOnly = true;
                                                                                 options.Cookie.IsEssential = true;
                                                                                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                                                                                 options.IdleTimeout = TimeSpan.FromMinutes(10);
                                                                             });
        public static void ConfigureResponseCache(this IServiceCollection services) => services.AddResponseCaching(options =>
                                                                                     {
                                                                                         options.UseCaseSensitivePaths = true;
                                                                                     });
        public static void AddDataProtectionService(this IServiceCollection services)
        {
            var keysLocation = Path.Combine(Directory.GetCurrentDirectory(), "App-Keys");

            services.AddDataProtection()
                    .DisableAutomaticKeyGeneration()
                    .PersistKeysToFileSystem(new DirectoryInfo(keysLocation))
                    .SetApplicationName("TBBProjectAp");

            services.AddSingleton<IDataProtection, DataProtectorFileSystem>();
        }

        public static IServiceCollection ConfigureTBBProjectHealth(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection ConfigureTBBProjectMvcLocalization(this IServiceCollection services)
        {
            services.AddTBBProjectInternationalization();
            var culture=new CultureInfo("en-US")
            {
                DateTimeFormat = new DateTimeFormatInfo(),
                NumberFormat = new NumberFormatInfo()
            };
            var defaultCulture = new RequestCulture(culture);
            var supportedCultures = new List<CultureInfo>
            {
                culture
            };
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    options.RequestCultureProviders.Clear();
                    options.DefaultRequestCulture = defaultCulture;
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
                });

            return services;
        }

    }
}

