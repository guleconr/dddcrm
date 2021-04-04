using System.Collections.Generic;

namespace TBBProject.Core.UiMessages
{
    public class TempDataMessageInfoContainer<TMessageInfo> : IMessageInfoContainer<TMessageInfo> where TMessageInfo : class, IMessageInfo
    {
        private readonly ITempDataProvider _tempDataProvider;
        private const string KEY = "TBBProject.Core.UiMessages.TempDataKey";
        public TempDataMessageInfoContainer(ITempDataProvider tempDataProvider) => _tempDataProvider = tempDataProvider;

        public void Add(TMessageInfo messageInfo)
        {
            var messages = _tempDataProvider.Get<IList<TMessageInfo>>(KEY) ?? new List<TMessageInfo>();
            messages.Add(messageInfo);
            _tempDataProvider.Add(KEY, messages);
        }

        public void Clear() => _tempDataProvider.Remove(KEY);

        public IEnumerable<TMessageInfo> Retrieve()
        {
            var messages = _tempDataProvider.Get<IList<TMessageInfo>>(KEY);
            Clear();
            return messages;
        }
    }
}
