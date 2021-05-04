using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface ILegislationAnnouncementDataLayer
    {
        IQueryable<LegislationAnnouncement> GetLegislationAnnouncement(long Id);

        void CreateLegislationAnnouncement(LegislationAnnouncement model);

        void UpdateLegislationAnnouncement(LegislationAnnouncement model);

        void DeleteLegislationAnnouncement(long Id);
        void AppLegislationAnnouncement(long Id);

        IQueryable<LegislationAnnouncement> GetLegislationAnnouncementAll();
        IQueryable<LegislationAnnouncementLang> GetLegislationAnnouncementLangAll(long legislationAnnouncementId);

        LegislationAnnouncementLang GetLegislationAnnouncementLang(long legislationAnnouncementId);

        LegislationAnnouncement GetLegislationAnnouncementWithId(long legislationAnnouncementId);




        void CreateLegislationAnnouncementLang(LegislationAnnouncementLang model);

        void UpdateLegislationAnnouncementLang(LegislationAnnouncementLang model);

        void DeleteLegislationAnnouncementLang(long Id);
    }
}
