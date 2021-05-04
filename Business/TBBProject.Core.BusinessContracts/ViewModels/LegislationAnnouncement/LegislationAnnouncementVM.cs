using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class LegislationAnnouncementVM
    {
        public long Id { get; set; }
        [Remote("ZamanCheck", "RemoteValidations", AdditionalFields = "EndReleaseDate", ErrorMessage = LocalizationCaptions.DateNotBigtoEnDate)]
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.EndReleaseDate)]
        [Remote("ZamanCheck2", "RemoteValidations", AdditionalFields = "ReleaseDate", ErrorMessage = LocalizationCaptions.DateNotSmalltoStartDate)]
        public DateTime EndReleaseDate { get; set; }
        public string EndReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<LegislationAnnouncementLangVM> LegislationAnnouncementLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        [Display(Name = LocalizationCaptions.LaLink)]
        public string Link { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.LAContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<LegislationAnnouncementFileVM> LegislationAnnouncementFile { get; set; }
        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> LegislationAnnouncementFileForm { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

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


    }
}
