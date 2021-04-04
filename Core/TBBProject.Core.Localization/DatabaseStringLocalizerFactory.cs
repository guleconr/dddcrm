using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Data;

namespace TBBProject.Core.Localization
{
    public class DatabaseStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceService _resourceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _uow;

        public DatabaseStringLocalizerFactory(IResourceService resourceService, IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
        {
            _uow = uow;
            _resourceService = resourceService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IStringLocalizer Create(Type resourceSource) => new DatabaseStringLocalizer(_resourceService, _httpContextAccessor, _uow);

        public IStringLocalizer Create(string baseName, string location) => new DatabaseStringLocalizer(_resourceService, _httpContextAccessor, _uow);
    }
}