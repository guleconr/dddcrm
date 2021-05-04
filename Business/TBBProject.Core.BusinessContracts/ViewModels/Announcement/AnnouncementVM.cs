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
    public class AnnouncementVM 
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        [Remote("ZamanCheck", "RemoteValidations", AdditionalFields = "EndReleaseDate", ErrorMessage = LocalizationCaptions.DateNotBigtoEnDate)]
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.EndReleaseDate)]
        [Remote("ZamanCheck2", "RemoteValidations", AdditionalFields = "ReleaseDate", ErrorMessage = LocalizationCaptions.DateNotSmalltoStartDate)]
        public DateTime? EndReleaseDate { get; set; }
        public string EndReleaseDateStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public bool IsRelease { get; set; }
        [Display(Name = LocalizationCaptions.AddSchedule)]
        public bool AddSchedule { get; set; }
        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public List<AnnouncementLangVM> AnnouncementLang { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        [Display(Name = LocalizationCaptions.AnnImage)]
        public IFormFile ImageForm { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.AnnContent)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Content { get; set; }
        [Display(Name = LocalizationCaptions.Image)]
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

        [Display(Name = LocalizationCaptions.Files)]
        public List<IFormFile> AnnouncementFileForm { get; set; }
        public List<AnnouncementFileVM> AnnouncementFile { get; set; }
        
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
        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public long AnnouncementTypeIdSearch { get; set; }

        [Display(Name = LocalizationCaptions.AnnouncementTypeId)]
        public string AnnouncementTypeStr { get; set; }
        [Display(Name = LocalizationCaptions.IsRelease)]
        public string IsReleaseStr { get; set; }

        [Display(Name = LocalizationCaptions.ApprovalType)]
        public string ApprovalStatusStr { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string TitleStr { get; set; }


    }
}
