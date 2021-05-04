using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TBBProject.Core.Business.Extension;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Common.Helper;
using TBBProject.Core.Data;
using TBBProject.Core.Web;
using TBBProject.Web.Extensions;

namespace TBBProject.Web
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
            var cs = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            services.AddDistributedMemoryCache();
            services.AddTierService(Configuration);
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddScoped<UrlLocalizationFilter>();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).
                AddNewtonsoftJson(i =>
                {
                    i.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    i.SerializerSettings.Formatting = Formatting.Indented;
                    i.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.AddKendo();
            return services.ConfigureApplicationServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ConfigureRequestPipeline();
            app.Use(async (context, next) =>
            {
                await next();
            });
        }
    }
}
