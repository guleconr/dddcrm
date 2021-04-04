using System.Collections.Generic;

namespace TBBProject.Core.UiMessages
{
    /// <summary>
    /// Container for message information.
    /// </summary>
    /// <typeparam name="TMessageInfo"></typeparam>
    public interface IMessageInfoContainer<TMessageInfo> where TMessageInfo : class, IMessageInfo
    {
        /// <summary>
        /// Adds message to container
        /// </summary>
        /// <param name="messageInfo"></param>
        void Add(TMessageInfo messageInfo);
        /// <summary>
        /// Clears container contenent
        /// </summary>
        void Clear();
        /// <summary>
        /// Retrieves notification informations
        /// </summary>
        /// <returns></returns>        
        IEnumerable<TMessageInfo> Retrieve();
    }
}
