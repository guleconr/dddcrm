using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace TBBProject.Core.DataLayer
{
    public class DefinitionsDataLayer : IDefinitionsDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Resource> _resourceRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IRepository<Icons> _iconsRepository;
        private readonly IRepository<AnnouncementType> _announcementTypeRepository;


        public DefinitionsDataLayer(IUnitOfWork uow)
        {
            _uow = uow;
            _resourceRepository = _uow.Repository<Resource>();
            _languageRepository = _uow.Repository<Language>();
            _iconsRepository = _uow.Repository<Icons>();
            _announcementTypeRepository = _uow.Repository<AnnouncementType>();
        }
        public async Task<List<Language>> GetLanguagesAsync()
        {
            return await _languageRepository.TableNoTracking.Include(i=>i.Resources).ToListAsync();
        }

        #region Resources
        public IQueryable<Resource> GetResources(long languageId)
        {
            var result = _resourceRepository.TableNoTracking.Include(i => i.Language).Where(i => i.LanguageId == languageId);
            return result;
        }

        public void CreateResources(Resource model)
        {
            _resourceRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateResources(Resource model)
        {
            var result = _resourceRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Value = model.Value;
            _resourceRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteResources(Resource model)
        {
            var result = _resourceRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _resourceRepository.Delete(result);
            _uow.SaveChanges();
        }
        public async Task<List<Resource>> GetResourcesAllAsync()
        {
            return await _resourceRepository.TableNoTracking.ToListAsync();
        }
        #endregion

        #region Announcement
        public IQueryable<AnnouncementType> GetAnnouncement()
        {
            var result = _announcementTypeRepository.TableNoTracking;
            return result;
        }

        public IQueryable<Language> GetLanguage()
        {
            var result = _languageRepository.TableNoTracking;
            return result;
        }

        public void CreateAnnouncement(AnnouncementType model)
        {
            _announcementTypeRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateAnnouncement(AnnouncementType model)
        {
            var result = _announcementTypeRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Name = model.Name;
            _announcementTypeRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteAnnouncement(AnnouncementType model)
        {
            var result = _announcementTypeRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _announcementTypeRepository.Delete(result);
            _uow.SaveChanges();
        }
        public async Task<List<AnnouncementType>> GetAnnouncementAllAsync()
        {
            return await _announcementTypeRepository.TableNoTracking.ToListAsync();
        }
        #endregion

        #region Icons
        public async Task<List<Icons>> GetIconsAsync()
        {
            return await _iconsRepository.TableNoTracking.ToListAsync();
        }
        #endregion

        #region Languages
        public IQueryable<Language> GetLanguages()
        {
            var result = _languageRepository.TableNoTracking;
            return result;
        }

        public void CreateLanguages(Language model)
        {
            _languageRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateLanguages(Language model)
        {
            var result = _languageRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Name = model.Name;
            result.Region = model.Region;
            result.Country = model.Country;
            result.CultureName = model.CultureName;
            _languageRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteLanguages(Language model)
        {
            var result = _languageRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _languageRepository.Delete(result);
            _uow.SaveChanges();
        }
        public async Task<List<Language>> GetLanguagesAllAsync()
        {
            return await _languageRepository.TableNoTracking.ToListAsync();
        }
        #endregion
    }
}
