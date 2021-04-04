namespace TBBProject.Core.Common
{
    public interface IEventPublisher
    {
        void Publish<T>(T message);
    }
}
