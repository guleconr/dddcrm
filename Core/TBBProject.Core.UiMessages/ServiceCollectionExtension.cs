using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace TBBProject.Core.UiMessages
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureMessage(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IMessageInfoContainerFactory, MessageInfoContainerFactory>();
            services.AddSingleton<ITempDataProvider, TempDataProvider>();
            services.AddScoped<IMessage, ToastrMessages>();
            var myAssembly = typeof(MessageViewComponent).Assembly;
            services.AddMvc().AddApplicationPart(myAssembly);
            return services;
        }
    }
}
