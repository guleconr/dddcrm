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
        DataSourceResult GetAnnouncementAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, int? ApprovalStatus ,UserVM user = null);
        DataSourceResult GetAnnouncementLangAll([DataSourceRequest] DataSourceRequest request, long announcementId, UserVM user = null);

        DataSourceResult GetAnnouncementApprovalAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate, UserVM user = null);

        AnnouncementLangVM GetAnnouncementLang(long announcementId);
        AnnouncementVM GetAnnouncement(long announcementId);



        DataSourceResult GetAnnouncement(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateAnnouncement(AnnouncementVM announcement,UserVM user);
        void UpdateAnnouncement(AnnouncementVM announcement);
        void DeleteAnnouncement(long Id);
        void AppAnnouncement(long Id);


        void CreateAnnouncementLang(AnnouncementLangVM announcement);
        void UpdateAnnouncementLang(AnnouncementLangVM announcement);
        void DeleteAnnouncementLang(long Id);
    }
}
