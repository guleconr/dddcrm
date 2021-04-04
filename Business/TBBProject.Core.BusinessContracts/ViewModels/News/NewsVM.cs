using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
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
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }
        public List<NewsLangVM> NewsLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.UserImage)]
        public byte?[] NewsImage { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public int? IsReleaseSearch { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public string IsReleaseStr { get; set; }

        [Display(Name = LocalizationCaptions.ApprovalType)]
        public string ApprovalStatusStr { get; set; }
    }
}
