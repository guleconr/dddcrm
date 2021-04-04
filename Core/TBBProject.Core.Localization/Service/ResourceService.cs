using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.Caching;

namespace TBBProject.Core.Localization
{
    public class ResourceService : IResourceService
    {
        private readonly ICacheProvider _cache;
        private readonly IServiceProvider _serviceProvider;

        public ResourceService(ICacheProvider cache, IServiceProvider serviceProvider)
        {
            _cache = cache;
            _serviceProvider = serviceProvider;
        }

        public Resource GetResourceByKeyAndCulture(string key, string culture)
        {
            Ensure.That("key", key).Is.NotNull().NotEmpty();
            Ensure.That("culture", culture).Is.NotNull().NotEmpty();

            var cacheKey = $"{key}:{culture}";

            var cached = _cache.Get<Resource>(cacheKey);

            if (cached.IsNull())
            {
                using (var unitOfWork = _serviceProvider.CreateUnitOfWork())
                {
                    var resourceRepository = unitOfWork.Repository<Resource>();
                    var resource = resourceRepository.TableNoTracking.FirstOrDefault(r => r.Name == key && r.Language.CultureName == culture);

                    if (resource == null)
                    {
                        // save new resource.
                        var lang = _cache.Get<Language>(culture);
                        if (lang == null)
                        {
                            var languageRepository = unitOfWork.Repository<Language>();
                            lang = languageRepository.TableNoTracking.FirstOrDefault(l => l.CultureName == culture);
                            _cache.Set(culture, lang);
                        }
                        var newResource = new Resource { Name = key, Value = $"{key} - N.A.", LanguageId = lang.Id };
                        resourceRepository.Insert(newResource);
                        unitOfWork.SaveChanges();

                        _cache.Set(cacheKey, newResource);
                    }
                    else
                    {
                        _cache.Set(cacheKey, resource);
                    }

                    return resource;
                }

                //using (var unitOfWork = _serviceProvider.CreateUnitOfWork())
                //{
                //    var resourceRepository = unitOfWork.Repository<Resource>();
                //    var resources = resourceRepository.TableNoTracking.Where(r => r.Language.CultureName == culture).ToList();

                //    Resource resourceToReturn = null;
                //    foreach (var resource in resources)
                //    {
                //        _cache.Set($"{resource.Name}:{culture}", resource);
                //        if (resource.Name == key)
                //        {
                //            resourceToReturn = resource;
                //        }
                //    }


                //    if (resourceToReturn == null)
                //    {
                //        var languageRepository = unitOfWork.Repository<Language>();

                //        // istenen 'culture' degistenine karsilik veritabaninda kayitli bir 'Language' olmasi zorunlu oldugu icin
                //        // 'First()' ile cekildi. 'culture' degerine karsilik 'Language' bulunamazsa hata atmali.
                //        var language = languageRepository.TableNoTracking.First(l => l.CultureName == culture);

                //        resourceRepository.Insert(new Resource() { Name = key, Value = $"{key} - Empty", LanguageId = language.Id });
                //        //unitOfWork.SaveChanges();
                //        //_cache.Set($"{key}:{culture}", new Resource { Name = key, Value = $"{key} - Empty" });
                //    }


                //    return resourceToReturn;
                //}
            }

            return cached;
        }

        public IEnumerable<LocalizedString> GetAllStrings(string culture)
        {
            Ensure.That("culture", culture).Is.NotNull().NotEmpty();

            using (var unitOfWork = _serviceProvider.CreateUnitOfWork())
            {
                var resourceRepository = unitOfWork.Repository<Resource>();

                var localizedStrings = resourceRepository.TableNoTracking.Include(l => l.Language)
                    .Where(r => r.Language.CultureName == culture)
                    .Select(s => new LocalizedString(s.Name, s.Value, false));

                return localizedStrings;
            }
        }
    }
}