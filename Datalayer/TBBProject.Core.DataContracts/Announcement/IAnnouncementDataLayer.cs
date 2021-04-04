using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IAnnouncementDataLayer
    {
        IQueryable<Announcement> GetAnnouncement(long Id);

        void CreateAnnouncement(Announcement model);

        void UpdateAnnouncement(Announcement model);

        void DeleteAnnouncement(long Id);
        IQueryable<Announcement> GetAnnouncementAllAsync();
        IQueryable<AnnouncementLang> GetAnnouncementLangAllAsync(long announcementId);

        AnnouncementLang GetAnnouncementLang(long announcementId);

        Announcement GetAnnouncementWithId(long announcementId);




        void CreateAnnouncementLang(AnnouncementLang model);

        void UpdateAnnouncementLang(AnnouncementLang model);

        void DeleteAnnouncementLang(AnnouncementLang model);
    }
}
