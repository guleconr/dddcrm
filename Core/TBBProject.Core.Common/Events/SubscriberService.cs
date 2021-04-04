using System;
using System.Collections.Generic;
using System.Linq; 

namespace TBBProject.Core.Common
{
	public class SubscriberService : ISubscriberService
    { 
        public virtual IList<IConsumer<T>> GetSubscribers<T>()
        {
            var iconsumer = typeof(IConsumer<T>);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
			                     .Where(p => iconsumer.IsAssignableFrom(p) && p.IsClass);

            var consumers = new List<IConsumer<T>>();

			foreach (var type in types)
            { 
				consumers.Add((IConsumer<T>) Activator.CreateInstance(type));
            } 

            return consumers; 
        } 
    }
}
