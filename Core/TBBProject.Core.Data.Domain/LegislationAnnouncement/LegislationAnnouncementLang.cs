using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
   public class LegislationAnnouncementLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public List<LegislationAnnouncementFile> LegislationAnnouncementFile { get; set; }
        public long LegislationAnnouncementId { get; set; }
        public LegislationAnnouncement LegislationAnnouncement { get; set; }
        public long UserId { get; set; }
        public Users User { get; set; }
    }
}
