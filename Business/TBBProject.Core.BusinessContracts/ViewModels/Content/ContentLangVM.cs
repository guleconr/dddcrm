using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
   public class ContentLangVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.ContentImage)]
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        [Display(Name = LocalizationCaptions.ContentImage)]
        public IFormFile ImageForm { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
        public long ContentId { get; set; }
        public ContentVM ContentObj { get; set; }
        public long UserId { get; set; }
        public UserVM User { get; set; }

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
