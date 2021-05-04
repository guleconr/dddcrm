using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
   public class AnnouncementLangVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.AnnContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.AnnImage)]
        public IFormFile ImageForm { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        public long AnnouncementId { get; set; }
        public AnnouncementVM Announcement { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> AnnouncementFileForm { get; set; }
        public List<AnnouncementFileVM> AnnouncementFile { get; set; }
        public long UserId { get; set; }
        public UserVM User { get; set; }
    }
}
