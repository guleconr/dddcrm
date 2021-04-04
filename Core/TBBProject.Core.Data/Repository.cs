using System; 
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
		private const string ParamName = "TBBProjectdatacontext";
		private readonly DbSet<TEntity> _dbSet;

        public Repository(IDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(ParamName);
            }
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Table => _dbSet;

        public IQueryable<TEntity> TableNoTracking => _dbSet.AsNoTracking(); 

        public void Delete(params TEntity[] entities)
        {
            Ensure.That("entities", entities).Is.NotNull().NotEmpty();

            _dbSet.AttachRange(entities);
            _dbSet.RemoveRange(entities);
        }

        public void Delete(TEntity entity)
        {
            Ensure.That("entity", entity).Is.NotNull();

            _dbSet.Remove(entity);
        }

        public void Insert(TEntity entity)
        {
            Ensure.That("entity", entity).Is.NotNull();

            _dbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            Ensure.That("Entities", entities).Is.NotNull().NotEmpty();

            foreach(var entity in entities)
            {
                _dbSet.Add(entity);
            }
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> query) => _dbSet.Where(query);

        public void Update(TEntity entity)
        {
            Ensure.That("entity", entity).Is.NotNull();

            _dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            Ensure.That("entities", entities).Is.NotNull().NotEmpty();

            foreach(var entity in entities)
            {
                _dbSet.Update(entity);
            }
        }
    }
}