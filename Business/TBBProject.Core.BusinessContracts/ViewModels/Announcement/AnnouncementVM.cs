using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AnnouncementVM 
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }
        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public long AnnouncementTypeId { get; set; }
        public AnnouncementTypeVM AnnouncementType { get; set; }
        public List<AnnouncementLangVM> AnnouncementLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Content)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.UserImage)]
        public byte[] Image { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDateSearch { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public int? IsReleaseSearch { get; set; }
        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public long AnnouncementTypeIdSearch { get; set; }

        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public string AnnouncementTypeStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public string IsReleaseStr { get; set; }

        [Display(Name = LocalizationCaptions.ApprovalType)]
        public string ApprovalStatusStr { get; set; }


    }
}
