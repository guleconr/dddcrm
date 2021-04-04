using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Common.Helper;
using TBBProject.Core.Data;
using TBBProject.Core.Web;

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
            GlobalConfiguration.WebRootPath = HostingEnvironment.WebRootPath;
            GlobalConfiguration.ContentRootPath = HostingEnvironment.ContentRootPath;
            var applicationSettings = Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
            GlobalConfiguration.PageSize = int.Parse(applicationSettings.PageSize);
            services.AddTransient(ec => new EncryptionService(new KeyInfo("45BLO2yoJkvBwz99kBEMlNkxvL40vUSGaqr/WBu3+Vg=", "Ou3fn+I9SVicGWMLkFEgZQ==")));
            var cs = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IRandomGenerator, RandomGenerator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddTierService(Configuration);
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
            );
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).
                AddNewtonsoftJson(i =>
                {
                    i.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    i.SerializerSettings.Formatting = Formatting.Indented;
                    i.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.AddKendo();
            services.AddAudit();
            return services.ConfigureApplicationServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ConfigureRequestPipeline();
        }
    }
}
