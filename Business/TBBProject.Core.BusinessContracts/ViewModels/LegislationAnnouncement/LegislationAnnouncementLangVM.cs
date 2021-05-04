using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
   public class LegislationAnnouncementLangVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.LAContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.LaLink)]
        public string Link { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<LegislationAnnouncementFileVM> LegislationAnnouncementFile { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> LegislationAnnouncementFileForm { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        public long LegislationAnnouncementId { get; set; }
        public LegislationAnnouncementVM LegislationAnnouncement { get; set; }
        public long UserId { get; set; }
        public UserVM User { get; set; }
    }
}
