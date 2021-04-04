using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.UiMessages
{
    public class MessageInfoContainerFactory : IMessageInfoContainerFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataProvider _tempDataProvider;

        public MessageInfoContainerFactory(ITempDataProvider tempDataProvider, IHttpContextAccessor httpContextAccessor)
        {
            _tempDataProvider = tempDataProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public IMessageInfoContainer<TMessageInfo> Create<TMessageInfo>()
            where TMessageInfo : class, IMessageInfo
        {
            if (_httpContextAccessor.HttpContext.Request.IsAjaxRequest())
            {
                return new InMemoryMessageInfoContainer<TMessageInfo>();
            }
            else
            {
                return new TempDataMessageInfoContainer<TMessageInfo>(_tempDataProvider);
            }
        }
    }
}
