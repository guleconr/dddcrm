using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Localization
{
    public class DatabaseStringLocalizer : IStringLocalizer
    {
        private readonly IResourceService _resourceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Language> _languageRepository;
        private readonly IUnitOfWork _uow;

        public DatabaseStringLocalizer(IResourceService resourceService, IHttpContextAccessor httpContextAccessor, IUnitOfWork uow)
        {
            _uow = uow;
            _resourceService = resourceService;
            _languageRepository = _uow.Repository<Language>();
            _httpContextAccessor = httpContextAccessor;
        }

        protected CultureInfo CurrentCulture => Thread.CurrentThread.CurrentCulture;

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            //var result = _resourceService.GetAllStrings(CurrentCulture.Name);
            //return result;
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //return this;

            throw new NotImplementedException();
        }

        public LocalizedString this[string name]
        {
            get
            {
                string culture = _httpContextAccessor.HttpContext.Session.GetString("ProjectCulture");

                var lang = new Language();
                if (culture == null)
                {
                    lang = _languageRepository.TableNoTracking.Where(i => i.IsDefault == true).FirstOrDefault();
                    culture = lang.CultureName.ToString();
                }
                var resource = _resourceService.GetResourceByKeyAndCulture(name, culture);
                return new LocalizedString(name, resource != null ? WebUtility.HtmlDecode(resource.Value ?? resource.Name) : name, resource == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];
    }
}

