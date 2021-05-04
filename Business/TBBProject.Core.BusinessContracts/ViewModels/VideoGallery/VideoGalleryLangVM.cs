using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class VideoGalleryLangVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.YoutubeUrl)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Url { get; set; }


        public long VideoGalleryId { get; set; }
        public VideoGalleryVM VideoGallery { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

    }
}
