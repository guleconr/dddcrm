using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface IAnnouncementBusiness
    {
        DataSourceResult GetAnnouncementAllAsync([DataSourceRequest] DataSourceRequest request, int? IsRelease, DateTime ReleaseDate, int AnnouncementType);
        DataSourceResult GetAnnouncementLangAllAsync([DataSourceRequest] DataSourceRequest request, long announcementId);

        AnnouncementLangVM GetAnnouncementLang(long announcementId);
        AnnouncementVM GetAnnouncement(long announcementId);



        DataSourceResult GetAnnouncement(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateAnnouncement(AnnouncementVM announcement);
        void UpdateAnnouncement(AnnouncementVM announcement);
        void DeleteAnnouncement(long Id);

        void CreateAnnouncementLang(AnnouncementLangVM announcement);
        void UpdateAnnouncementLang(AnnouncementLangVM announcement);
        void DeleteAnnouncementLang(AnnouncementLangVM announcement);
    }
}
