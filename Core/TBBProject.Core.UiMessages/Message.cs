using System.Collections.Generic;

namespace TBBProject.Core.UiMessages
{
    public abstract class Message<TMessageInfo> : IMessage
        where TMessageInfo : class, IMessageInfo
    {

        private readonly IMessageInfoContainer<TMessageInfo> _messageInfoContainer;
        public Message(IMessageInfoContainerFactory factory) => _messageInfoContainer = factory.Create<TMessageInfo>();
        public void Add(IMessageInfo messageInfo) => _messageInfoContainer.Add(messageInfo as TMessageInfo);
        public void Clear() => _messageInfoContainer.Clear();
        public IEnumerable<IMessageInfo> Get() => _messageInfoContainer.Retrieve();
        public abstract void Error(IErrorMessage message);
        public abstract void Info(IInfoMessage message);
        public abstract void Success(ISuccessMessage message);
        public abstract void Warning(IWarningMessage message);
        public abstract void Info(string title, string message);
        public abstract void Warning(string title, string message);
        public abstract void Success(string title, string message);
        public abstract void Error(string title, string message);
    }
}
