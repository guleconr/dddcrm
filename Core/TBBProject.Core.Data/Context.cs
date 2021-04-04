using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public abstract class Context : DbContext, IDataContext
    {
        protected Context(DbContextOptions options) : base(options)
        {
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity => base.Set<TEntity>();
        public override int SaveChanges() => base.SaveChanges();
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}