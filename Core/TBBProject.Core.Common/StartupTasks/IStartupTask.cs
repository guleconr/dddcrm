namespace TBBProject.Core.Common
{
    using System;
    using System.Threading.Tasks;

    public interface IStartupTask
    {
        int Order { get; }

        Task InvokeAsync(IServiceProvider serviceProvider);
    }
}
