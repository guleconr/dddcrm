using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public class UnitofWork : IUnitOfWork
    {
        private IDataContext _dataContext;

        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public UnitofWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<Type, object>();
        }  

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_dataContext);
            }

            return (IRepository<TEntity>) _repositories[type];
        }

        public int SaveChanges() => _dataContext.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _dataContext.SaveChangesAsync();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    if (_dataContext != null)
                    {
                        _dataContext.Dispose();
                        _dataContext = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                } 
            }

            _disposed = true;
        }
    }
}