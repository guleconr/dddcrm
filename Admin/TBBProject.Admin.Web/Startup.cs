using System;
using System.Globalization;
using AutoMapper;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TBBProject.Admin.Web.Helpers;
using TBBProject.Core.Business.Extension;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Data;
using TBBProject.Core.Logging;
using TBBProject.Core.Web;

namespace TBBProject.Admin.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GlobalConfiguration.WebRootPath = HostingEnvironment.WebRootPath;
            GlobalConfiguration.ContentRootPath = HostingEnvironment.ContentRootPath;
            var applicationSettings = Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
            GlobalConfiguration.PageSize = int.Parse(applicationSettings.PageSize);
            var cs = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IRandomGenerator, RandomGenerator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            //services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddTransient(ec => new EncryptionService(new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddTierService(Configuration);
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddDNTCaptcha(options =>
                options.UseCookieStorageProvider()
                    .ShowThousandsSeparators(false)
            );

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
            );
            services.AddScoped<ProcessLoggerFilter>();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).
                AddNewtonsoftJson(i =>
                {
                    i.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    i.SerializerSettings.Formatting = Formatting.Indented;
                    i.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
            });
            services.AddKendo();
            services.AddAudit();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/denied");
                });
            return services.ConfigureApplicationServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ConfigureRequestPipeline();
            app.UseSession();
        }
    }
}
