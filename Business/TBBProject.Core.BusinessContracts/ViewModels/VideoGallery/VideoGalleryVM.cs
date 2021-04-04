using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class VideoGalleryVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDate { get; set; }
        public List<VideoGalleryLangVM> VideoGalleryLang { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Url)]
        public string Url { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }

    }
}
