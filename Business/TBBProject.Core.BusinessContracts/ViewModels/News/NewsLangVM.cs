using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class NewsLangVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.HeadLine)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string HeadLine { get; set; }
        [Display(Name = LocalizationCaptions.NewsContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.SliderImage)]
        public IFormFile SliderImageForm { get; set; }
        public byte[] SliderImage { get; set; }
        [Display(Name = LocalizationCaptions.NewsImage)]
        public byte[] NewsImage { get; set; }
        [Display(Name = LocalizationCaptions.NewsImage)]
        public IFormFile NewsImageForm { get; set; }

        [Display(Name = LocalizationCaptions.NewsGallery)]
        public List<NewsGalleryVM> NewsGallery { get; set; }
        [Display(Name = LocalizationCaptions.NewsGallery)]
        public List<IFormFile> NewsGalleryForm { get; set; }
        public string SliderImageName { get; set; }
        public string NewsImageName { get; set; }

        [Display(Name = LocalizationCaptions.NewsFile)]
        public long NewsId { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        public long UserId { get; set; }
        public UserVM User { get; set; }
    }
}
