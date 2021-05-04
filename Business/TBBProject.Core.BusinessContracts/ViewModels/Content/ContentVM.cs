using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class ContentVM 
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }

        public List<ContentLangVM> ContentLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.ContentContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.ContentImage)]
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        [Display(Name = LocalizationCaptions.ContentImage)]
        public IFormFile ImageForm { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }
        public string ReleaseDateSearchStr { get; set; }
        [Display(Name = LocalizationCaptions.EndDate)]
        public DateTime EndDateSearch { get; set; }
        public string EndDateSearchStr { get; set; }

        [Display(Name = LocalizationCaptions.IsRelease)]
        public int? IsReleaseSearch { get; set; }

        [Display(Name = LocalizationCaptions.IsRelease)]
        public string IsReleaseStr { get; set; }

        [Display(Name = LocalizationCaptions.ApprovalType)]
        public string ApprovalStatusStr { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string TitleStr { get; set; }

        [Display(Name = LocalizationCaptions.Files)]
        public List<ContentFileVM> ContentFile { get; set; }
        [Display(Name = LocalizationCaptions.Gallery)]
        public List<ContentGalleryVM> ContentGallery { get; set; }

        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> ContentFileForm { get; set; }
        [Display(Name = LocalizationCaptions.Gallery)]
        public List<IFormFile> ContentGalleryForm { get; set; }

    }
}
