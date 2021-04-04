using System.Diagnostics.CodeAnalysis;

namespace TBBProject.Core.Common
{
    public class Event<T> where T: IEventEntity
    {
        public Event(T entity)
        {
			Entity = entity;
        }

		public T Entity { get; }
    }
}
