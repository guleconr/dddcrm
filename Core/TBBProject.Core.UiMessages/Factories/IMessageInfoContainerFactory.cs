namespace TBBProject.Core.UiMessages
{
    public interface IMessageInfoContainerFactory
    {
        IMessageInfoContainer<TMessageInfo> Create<TMessageInfo>() where TMessageInfo : class, IMessageInfo;
    }
}
