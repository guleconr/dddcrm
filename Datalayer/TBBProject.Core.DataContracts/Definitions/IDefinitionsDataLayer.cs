using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IDefinitionsDataLayer
    {
        Task<List<Language>> GetLanguagesAsync();

        #region Resources
        IQueryable<Resource> GetResources(long languageId);

        void CreateResources(Resource model);

        void UpdateResources(Resource model);

        void DeleteResources(Resource model);
        Task<List<Resource>> GetResourcesAllAsync();
        #endregion

        #region Resources
        IQueryable<AnnouncementType> GetAnnouncement();
        IQueryable<Language> GetLanguage();


        void CreateAnnouncement(AnnouncementType model);

        void UpdateAnnouncement(AnnouncementType model);

        void DeleteAnnouncement(AnnouncementType model);
        Task<List<AnnouncementType>> GetAnnouncementAllAsync();
        #endregion

        #region Icons
        Task<List<Icons>> GetIconsAsync();
        #endregion



        #region Languages
        IQueryable<Language> GetLanguages();
        void CreateLanguages(Language model);
        void UpdateLanguages(Language model);
        void DeleteLanguages(Language model);
        Task<List<Language>> GetLanguagesAllAsync();
        #endregion
    }
}
