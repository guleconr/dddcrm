namespace TBBProject.Core.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using TBBProject.Core.Common;

    public static class FileProviderExtensions
    {
        public static IServiceCollection AddFileProvider(this IServiceCollection services)
        {
            services.AddSingleton<IFileSystemFileProvider, FileSystemProvider>();
            services.AddSingleton<IFileProvider, FileSystemProvider>();
            return services;
        }

        public static IApplicationBuilder UseFileProviderForStaticFiles(this IApplicationBuilder app)
        {
            var fileProvider = TBBProjectContext.Current.Resolve<IFileSystemFileProvider>();

            var compositeFileProvider = new CompositeFileProvider(fileProvider);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = compositeFileProvider,
            });

            return app;
        }
    }
}
