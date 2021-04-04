using System;
using Microsoft.Extensions.DependencyInjection;
using TBBProject.Core.Data;

namespace TBBProject.Core.Localization
{
    public static class InstanceFactoryExtensions
    {
        public static IUnitOfWork CreateUnitOfWork(this IServiceProvider serviceProvider)
        {
            var serviceScope = serviceProvider.CreateScope(); 
            var context = serviceScope.ServiceProvider.GetService<DataContext>(); 
            return new UnitofWork(context);
        }
    }
}