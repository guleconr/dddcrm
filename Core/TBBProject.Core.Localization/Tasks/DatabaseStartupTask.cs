//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using TBBProject.Core.Common;
//using TBBProject.Core.Data.Domain;
//using TBBProject.Core.Caching;
//using TBBProject.Core.Data;
//using Microsoft.Extensions.DependencyInjection;

//namespace TBBProject.Core.Localization
//{
//    internal class DatabaseStartupTask // : IStartupTask
//    {
//        public int Order => 1; 

//        public async Task InvokeAsync(IServiceProvider serviceProvider)
//        {
//            using (var scope = serviceProvider.CreateScope())
//            {
//                var _cache = scope.ServiceProvider.GetService<ICacheProvider>();

//                var unitOfWork = serviceProvider.CreateUnitOfWork();
//                var languageRepo = unitOfWork.Repository<Language>();
//                var resourceRepo = unitOfWork.Repository<Resource>();

//                PopulateEnglish(languageRepo, resourceRepo);
//                PopulateTurkish(languageRepo, resourceRepo);

//                var resources = resourceRepo.TableNoTracking.Include(l => l.Language);

//                foreach (var resource in resources)
//                {
//                    var cacheKey = $"{resource.Name}:{resource.Language.CultureName}";
//                    _cache.Set(cacheKey, resource);
//                }

//                await unitOfWork.SaveChangesAsync();
//            }
//        } 

//        // TODO: test
//        private void PopulateEnglish(IRepository<Language> languageRepo, IRepository<Resource> resourceRepo)
//        {
//            if (!languageRepo.TableNoTracking.Any(o => o.CultureName == "en-US"))
//            {
//                var language = new Language
//                {
//                    Country = "United States",
//                    CultureName = "en-US",
//                    Name = "English - USA",
//                    Region = "United States",
//                    IsDefault = true
//                };

//                languageRepo.Insert(language);

//                var resureceList = new List<Resource>
//                {
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.MissingBindRequiredValueAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.MissingKeyOrValueAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.MissingKeyOrValueAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueIsInvalidAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.ValueIsInvalidAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueMustBeANumberAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.ValueMustBeANumberAccessor },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, Value = Constants.DefaultMessage.ModelBindingMessage.ValueMustNotBeNullAccessor },

//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.InvalidEmail, Value = Constants.DefaultMessage.AttributeMessage.InvalidEmail },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.MustMatchRegex, Value = Constants.DefaultMessage.AttributeMessage.MustMatchRegex },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.PasswordNoMatch, Value = Constants.DefaultMessage.AttributeMessage.PasswordNoMatch },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.Range, Value = Constants.DefaultMessage.AttributeMessage.Range },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.Required, Value = Constants.DefaultMessage.AttributeMessage.Required },

//                    new Resource { LanguageId = language.Id, Name = "Browse.Jobs", Value = "Browse Jobs" },
//                    new Resource { LanguageId = language.Id, Name = "Browse.Categories", Value = "Browse Categories" }
                    
//                };

//                resureceList.ForEach(resourceRepo.Insert);
//            }
//        } 

//        private void PopulateTurkish(IRepository<Language> languageRepo, IRepository<Resource> resourceRepo)
//        {
//            if (!languageRepo.TableNoTracking.Any(k => k.CultureName == "tr-TR"))
//            {
//                var language = new Language
//                {
//                    Country = "Turkey",
//                    CultureName = "tr-TR",
//                    Name = "Turkish",
//                    Region = "Turkey",
//                    IsDefault = false
//                };

//                languageRepo.Insert(language);

//                var resureceList = new List<Resource>
//                {
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.AttemptedValueIsInvalidAccessor, Value = "'{0}' de??eri i??in {1} ge??erli de??il." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.MissingBindRequiredValueAccessor, Value = "'{0}' alan?? i??in bir de??er sa??lanamad??." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.MissingKeyOrValueAccessor, Value = "Bir de??er gerekli." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.UnknownValueIsInvalidAccessor, Value = "Sa??lanan de??er, {0} i??in ge??ersiz." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueIsInvalidAccessor, Value = "'{0}' de??eri ge??ersiz." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueMustBeANumberAccessor, Value = "{0} alan?? bir say?? olmal??d??r." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.ModelBindingMessage.ValueMustNotBeNullAccessor, Value = "Null de??eri ge??ersiz." },

//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.InvalidEmail, Value = "{0} alan?? ge??erli bir e-posta adresi de??il." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.MustMatchRegex, Value = "{0} alan??, '{1}' ifadesinin d??zenli ifadesiyle e??le??melidir." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.PasswordNoMatch, Value = "??ifre ve onay ??ifresi uyu??muyor." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.Range, Value = "{0} alan??, {2} ifadesinden k??????k ve {1} ifadesinden b??y??k olmal??d??r." },
//                    new Resource { LanguageId = language.Id, Name = Constants.DefaultMessage.AttributeMessage.Required, Value = "{0} alan?? bo?? olamaz." },

//                    new Resource { LanguageId = language.Id, Name = "Browse.Jobs", Value = "Browse Kunfts" }
//                };

//                resureceList.ForEach(resourceRepo.Insert);
//            }
//        }
//    }
//}