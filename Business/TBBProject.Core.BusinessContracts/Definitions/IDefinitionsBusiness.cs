using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.BusinessContracts
{
    public interface IDefinitionsBusiness
    {
        DataSourceResult GetResources(long languageId, [DataSourceRequest] DataSourceRequest request);
        List<LanguageVM> GetLanguageList();

        void CreateResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources);
        void UpdateResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources);
        void DeleteResources([DataSourceRequest] DataSourceRequest request, IEnumerable<ResourcesVM> resources);
        Task<List<IconVM>> Get_IconsAsyc();
        Task<List<ResourcesVM>> GetResourcesAllAsync();

        Task<List<AnnouncementTypeVM>> GetAnnouncementAllAsync();
        DataSourceResult GetAnnouncement([DataSourceRequest] DataSourceRequest request);
        void CreateAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement);
        void UpdateAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement);
        void DeleteAnnouncement([DataSourceRequest] DataSourceRequest request, IEnumerable<AnnouncementTypeVM> announcement);
        List<AnnouncementTypeVM> Get_AnnouncementList();

        Task<List<LanguageVM>> GetLanguagesAsync();
        public DataSourceResult GetLanguages([DataSourceRequest] DataSourceRequest request);
        void CreateLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources);
        void UpdateLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources);
        void DeleteLanguages([DataSourceRequest] DataSourceRequest request, IEnumerable<LanguageVM> resources);
        Task<List<LanguageVM>> GetLanguagesAllAsync();
       
    }
}
