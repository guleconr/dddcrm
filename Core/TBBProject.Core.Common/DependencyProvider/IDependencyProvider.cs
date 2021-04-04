namespace TBBProject.Core.Common
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public interface IDependencyProvider
    {
        int Order { get; }

        void Register(IServiceCollection services, ITypeLocator typeLocator, IConfiguration configuration);
    }
}
