using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    } 
}