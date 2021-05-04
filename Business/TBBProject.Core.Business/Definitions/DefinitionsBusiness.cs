using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace TBBProject.Core.Business
{

    public class DefinitionsBusiness : IDefinitionsBusiness
    {
        private readonly IDefinitionsDataLayer _definitionsDataLayer;
        private readonly IMapper _mapper;
        public DefinitionsBusiness(IDefinitionsDataLayer definitionsDataLayer, IMapper mapper)
        {
            _definitionsDataLayer = definitionsDataLayer;
            _mapper = mapper;
        }
        #region Resources
        public DataSourceResult GetResources(long languageId, [DataSourceRequest] DataSourceRequest request)
        {
            var resources = _definitionsDataLayer.GetResources(languageId).OrderBy(i => i.Name).ToDataSourceResult(request);
            resources.Data = _mapper.Map<List<ResourcesVM>>(( resources.Data ));
            return resources;
        }
     
        public void CreateResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources)
        {
            var eResources = _mapper.Map<List<Resource>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.CreateResources(item);
            }
        }
        public void UpdateResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources)
        {
            var eResources = _mapper.Map<List<Resource>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.UpdateResources(item);
            }
        }
        public void DeleteResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources)
        {
            var eResources = _mapper.Map<List<Resource>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.DeleteResources(item);
            }
        }
        public async Task<List<ResourcesVM>> GetResourcesAllAsync()
        {
            var resources = await _definitionsDataLayer.GetResourcesAllAsync();
            var result = _mapper.Map<List<ResourcesVM>>(( resources ));
            return result;
        }
        #endregion

        #region Icons
        public async Task<List<IconVM>> Get_IconsAsyc()
        {
            var icons = await _definitionsDataLayer.GetIconsAsync();
            return _mapper.Map<List<IconVM>>(icons);
        }
        #endregion
        #region Languages
        public async Task<List<LanguageVM>> GetLanguagesAsync()
        {
            var languages = await _definitionsDataLayer.GetLanguagesAsync();
            return _mapper.Map<List<LanguageVM>>(languages);
        }
        public DataSourceResult GetLanguages([DataSourceRequest] DataSourceRequest request)
        {
            var resources = _definitionsDataLayer.GetLanguages().OrderBy(i => i.Name).ToDataSourceResult(request);
            resources.Data = _mapper.Map<List<LanguageVM>>(( resources.Data ));
            return resources;
        }

        public void CreateLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources)
        {
            var eResources = _mapper.Map<List<Language>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.CreateLanguages(item);
            }
        }
        public void UpdateLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources)
        {
            var eResources = _mapper.Map<List<Language>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.UpdateLanguages(item);
            }
        }
        public void DeleteLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources)
        {
            var eResources = _mapper.Map<List<Language>>(( resources ));
            foreach (var item in eResources)
            {
                _definitionsDataLayer.DeleteLanguages(item);
            }
        }
        public async Task<List<LanguageVM>> GetLanguagesAllAsync()
        {
            var resources = await _definitionsDataLayer.GetLanguagesAllAsync();
            var result = _mapper.Map<List<LanguageVM>>(( resources ));
            return result;
        }
        #endregion
        #region Announcement
        public DataSourceResult GetAnnouncement( [DataSourceRequest] DataSourceRequest request)
        {
            var announcement = _definitionsDataLayer.GetAnnouncement().OrderBy(i => i.Name).ToDataSourceResult(request);
            announcement.Data = _mapper.Map<List<AnnouncementTypeVM>>(announcement.Data);
            return announcement;
        }
        public List<AnnouncementTypeVM> Get_AnnouncementList()
        {
            var announcement = _definitionsDataLayer.GetAnnouncement().OrderBy(i => i.Name).ToList();
            var result = _mapper.Map<List<AnnouncementTypeVM>>(announcement);
            return result;
        }

        public List<LanguageVM> GetLanguageList()
        {
            var languages = _definitionsDataLayer.GetLanguage().OrderBy(i => i.Name).ToList();
            return _mapper.Map<List<LanguageVM>>(languages);
        }

        public void CreateAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement)
        {
            var eannouncement = _mapper.Map<List<AnnouncementType>>((announcement));
            foreach (var item in eannouncement)
            {
                _definitionsDataLayer.CreateAnnouncement(item);
            }
        }
        public void UpdateAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement)
        {
            var eannouncement = _mapper.Map<List<AnnouncementType>>(( announcement ));
            foreach (var item in eannouncement)
            {
                _definitionsDataLayer.UpdateAnnouncement(item);
            }
        }
        public void DeleteAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement)
        {
            var eannouncement = _mapper.Map<List<AnnouncementType>>(( announcement ));
            foreach (var item in eannouncement)
            {
                _definitionsDataLayer.DeleteAnnouncement(item);
            }
        }
        public async Task<List<AnnouncementTypeVM>> GetAnnouncementAllAsync()
        {
            var resources = await _definitionsDataLayer.GetAnnouncementAllAsync();
            var result = _mapper.Map<List<AnnouncementTypeVM>>(( resources ));
            return result;
        }
        #endregion
    }
}
