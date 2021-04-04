using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
   public class AnnouncementLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public long AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
    }
}
