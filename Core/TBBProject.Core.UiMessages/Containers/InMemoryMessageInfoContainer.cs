using System.Collections.Generic;

namespace TBBProject.Core.UiMessages
{
    public class InMemoryMessageInfoContainer<TMessageInfo> : IMessageInfoContainer<TMessageInfo> where TMessageInfo : class, IMessageInfo
    {
        private IList<TMessageInfo> _data;
        public InMemoryMessageInfoContainer() => _data = new List<TMessageInfo>();

        public void Add(TMessageInfo messageInfo) => _data.Add(messageInfo);

        public void Clear() => _data.Clear();

        public IEnumerable<TMessageInfo> Retrieve() => _data;
    }
}
