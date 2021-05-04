using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.Data
{
    public partial class DataContext : Context
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ProcessLogs> ProcessLogs { get; set; }
        public DbSet<Authority> Authority { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserAuthority> UserAuthority { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RoleAuthority> RoleAuthority { get; set; }
        public DbSet<Icons> Icons { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementFile> AnnouncementFile { get; set; }
        public DbSet<AnnouncementType> AnnouncementType { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsLang> NewsLang { get; set; }
        public DbSet<NewsGallery> NewsGallery { get; set; }
        public DbSet<NewsFile> NewsFile { get; set; }

        public DbSet<VideoGallery> VideoGallery { get; set; }
        public DbSet<VideoGalleryLang> VideoGalleryLang { get; set; }
        public DbSet<AcademicSchedule> AcademicSchedule { get; set; }
        public DbSet<AcademicScheduleLang> AcademicScheduleLang { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentLang> ContentLang { get; set; }
        public DbSet<ContentFile> ContentFile { get; set; }
        public DbSet<ContentGallery> ContentGallery { get; set; }

        public DbSet<LegislationAnnouncement> LegislationAnnouncement { get; set; }
        public DbSet<LegislationAnnouncement> LegislationAnnouncementLang { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<District> District { get; set; }


        public DbSet<DynamicQuestions> DynamicQuestions { get; set; }
        public DbSet<DynamicQuestionsItem> DynamicQuestionsItem { get; set; }
        public DbSet<DynamicQuestionsResult> DynamicQuestionsResult { get; set; }

        
    }
}
