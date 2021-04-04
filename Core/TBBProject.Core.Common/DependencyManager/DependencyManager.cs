namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyManager : IManageDependencies
    {
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        protected IServiceProvider _serviceProvider { get; set; }
        
        protected IServiceProvider GetServiceProvider()
        {
            var httpContextAccessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor.HttpContext;

            return httpContext?.RequestServices ?? ServiceProvider;
        }

        // TODO: revisit
        protected virtual void RunStartupTasks(ITypeLocator typeFinder)
        {
            var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            var instances = startupTasks
                .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask.Order);

            foreach (var task in instances)
            {
                task.InvokeAsync(ServiceProvider);
            }
        }
        protected virtual IServiceProvider RegisterDependencies(IServiceCollection services, ITypeLocator typeLocator, IConfiguration configuration)
        {
            //services.AddSingleton<IManageDependencies, DependencyManager>();
            services.AddSingleton(typeof(IManageDependencies), this);
            services.AddSingleton(typeof(ITypeLocator), typeLocator);

            var dependencyProviders = typeLocator.FindClassesOfType<IDependencyProvider>();

            var instances = dependencyProviders
                .Select(dependencyProvider => (IDependencyProvider)Activator.CreateInstance(dependencyProvider))
                .OrderBy(dependencyProvider => dependencyProvider.Order);

            foreach (var dependencyProvider in instances)
            {
                dependencyProvider.Register(services, typeLocator, configuration);
            }

            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider;
        }

        protected Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
            {
                return assembly;
            }

            var tf = Resolve<ITypeLocator>();
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        public void Initialize(IServiceCollection services)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            CommonHelper.FileProvider = new FileSystemProvider(hostingEnvironment);
        }

        public T Resolve<T>()
            where T : class => GetServiceProvider().GetService<T>();

        public object Resolve(Type type) => GetServiceProvider().GetService(type);

        public IEnumerable<T> ResolveAll<T>() => GetServiceProvider().GetServices<T>();

        public object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                        {
                            throw new Exception("Dependency not known at run time. ");
                        }

                        return service;
                    });

                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new Exception("No constructor found.", innerException);
        }

        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            var typeFinder = Resolve<ITypeLocator>();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
            {
                instance.Configure(application);
            }
        }

        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var typeFinder = new WebApplicationTypeLocator();
            var startupConfigurations = typeFinder.FindClassesOfType<IAppStartup>();

            var instances = startupConfigurations
                .Select(startup => (IAppStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
            {
                instance.ConfigureServices(services);
            }

            RegisterDependencies(services, typeFinder, configuration);

            RunStartupTasks(typeFinder);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return _serviceProvider;
        }
    }
}
