using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;

namespace TBBProject.Core.BusinessContracts
{
    public interface ILegislationAnnouncementBusiness
    {
        DataSourceResult GetLegislationAnnouncementAll([DataSourceRequest] DataSourceRequest request, int? IsRelease, string ReleaseDate, string EndDate);
        DataSourceResult GetLegislationAnnouncementLangAll([DataSourceRequest] DataSourceRequest request, long legislationAnnouncementId);

        LegislationAnnouncementLangVM GetLegislationAnnouncementLang(long legislationAnnouncementId);
        LegislationAnnouncementVM GetLegislationAnnouncement(long legislationAnnouncementId);



        DataSourceResult GetLegislationAnnouncement(long languageId, [DataSourceRequest] DataSourceRequest request);
        void CreateLegislationAnnouncement(LegislationAnnouncementVM legislationAnnouncement,UserVM user);
        void UpdateLegislationAnnouncement(LegislationAnnouncementVM legislationAnnouncement);
        void DeleteLegislationAnnouncement(long Id);
        void AppLegislationAnnouncement(long Id);


        void CreateLegislationAnnouncementLang(LegislationAnnouncementLangVM legislationAnnouncement);
        void UpdateLegislationAnnouncementLang(LegislationAnnouncementLangVM legislationAnnouncement);
        void DeleteLegislationAnnouncementLang(long Id);
    }
}
