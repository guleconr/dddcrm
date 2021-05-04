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
        public string ReleaseDateStr { get; set; }
        public List<VideoGalleryLangVM> VideoGalleryLang { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.YoutubeUrl)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Url { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }
        public string ReleaseDateSearchStr { get; set; }

        [Display(Name = LocalizationCaptions.EndDate)]
        public DateTime EndDateSearch { get; set; }
        public string EndDateSearchStr { get; set; }


        [Display(Name = LocalizationCaptions.Title)]
        public string TitleStr { get; set; }

    }
}
