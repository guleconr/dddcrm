using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class Announcement : IEntity
    {
        [Key] 
        public long Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ? EndReleaseDate { get; set; }
        public bool IsRelease { get; set; }
        public bool AddSchedule { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public List<AnnouncementLang> AnnouncementLang { get; set; }
    }
}
