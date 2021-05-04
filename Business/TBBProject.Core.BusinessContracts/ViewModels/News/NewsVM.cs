using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class NewsVM
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }
        public List<NewsLangVM> NewsLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.NewsContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.HeadLine)]
        public string HeadLine { get; set; }
        [Display(Name = LocalizationCaptions.SliderImage)]
        public IFormFile SliderImageForm { get; set; }
        [Display(Name = LocalizationCaptions.SliderImage)]
        public byte[] SliderImage { get; set; }
        [Display(Name = LocalizationCaptions.NewsImage)]
        public IFormFile NewsImageForm { get; set; }
        [Display(Name = LocalizationCaptions.NewsImage)]
        public byte[] NewsImage { get; set; }
        public string SliderImageName { get; set; }
        public string NewsImageName { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        [Display(Name = LocalizationCaptions.NewsGallery)]
        public List<NewsGalleryVM> NewsGallery { get; set; }
        [Display(Name = LocalizationCaptions.NewsGallery)]
        public List<IFormFile> NewsGalleryForm { get; set; }

        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> NewsFileForm { get; set; }
        public List<NewsFileVM> NewsFile { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }
        public string ReleaseDateSearchStr { get; set; }
        [Display(Name = LocalizationCaptions.EndDate)]
        public DateTime EndDateSearch { get; set; }
        public string EndDateSearchStr { get; set; }

        [Display(Name = LocalizationCaptions.IsRelease)]
        public int? IsReleaseSearch { get; set; }
        [Display(Name = LocalizationCaptions.ApprovalType)]
        public int? ApprovalStatusSearch { get; set; }

        [Display(Name = LocalizationCaptions.IsRelease)]
        public string IsReleaseStr { get; set; }

        [Display(Name = LocalizationCaptions.ApprovalType)]
        public string ApprovalStatusStr { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string TitleStr { get; set; }
    }
}
