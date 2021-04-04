using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entites);
        void Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
    }
}