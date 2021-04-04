using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
   public class AnnouncementLangVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.UserImage)]
        public byte[] Image { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        public long AnnouncementId { get; set; }
        public AnnouncementVM Announcement { get; set; }
    }
}
