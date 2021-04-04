using System.Linq;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public static class LinqExtensions
    {
        public static T GetNext<T>(this IQueryable<T> list, T current) where T : IEntity
        {
            if(current == null)
            {
                return default;
            }

            var first = list.Where(e => e.Id > current.Id);
            var ordered = first.OrderBy(e => e.Id);
            var result = ordered.FirstOrDefault();

            return result;              
        }

        public static T GetPrevious<T>(this IQueryable<T> list, T current) where T : IEntity
        {
            if(current == null)
            {
                return default;
            }

            var first = list.Where(e => e.Id < current.Id);
            var ordered = first.OrderByDescending(e => e.Id);
            var result = ordered.FirstOrDefault();

            return result;
        }
    }
}
