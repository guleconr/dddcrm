using System;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}