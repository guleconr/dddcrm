using System.Collections.Generic;

namespace TBBProject.Core.Common
{
    public interface ISubscriberService
    {
        IList<IConsumer<T>> GetSubscribers<T>();
    }
}
