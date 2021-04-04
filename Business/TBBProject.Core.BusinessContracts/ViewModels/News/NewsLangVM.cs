using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class NewsLangVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        public string HeadLine { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        public string Content { get; set; }
        public byte?[] SliderImage { get; set; }
        public byte?[] NewsImage { get; set; }
        public List<NewsGalleryVM> NewsGallery { get; set; }
        public long NewsId { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
    }
}
