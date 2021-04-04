using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.HtmlToPdf
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHtmlToPdf(this IServiceCollection services)
        {
            services.AddTransient<IConverHtmlToPdf, ConvertHtmlToPdf>();
            services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
            services.AddTransient<IPdfConverter, HtmlToPdfConverter>();
             
            return services;
        }
    }
}
