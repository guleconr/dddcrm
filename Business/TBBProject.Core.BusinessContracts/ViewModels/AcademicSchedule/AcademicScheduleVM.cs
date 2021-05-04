using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AcademicScheduleVM 
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.StartDate)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [Remote("DateTimeCheck", "RemoteValidations", AdditionalFields = "EndDate", ErrorMessage = LocalizationCaptions.DateNotBigtoEnDate)]
        public DateTime StartDate { get; set; }
        public string StartDateStr { get; set; }
        [Display(Name = LocalizationCaptions.EndDate)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [Remote("DateTimeCheck2", "RemoteValidations", AdditionalFields = "StartDate", ErrorMessage = LocalizationCaptions.DateNotSmalltoStartDate)]
        public DateTime EndDate { get; set; }
        public string EndDateStr { get; set; }
        [Display(Name = LocalizationCaptions.Quota)]
        public long Quota { get; set; }
        public List<AcademicScheduleLangVM> AcademicScheduleLang { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.AscheduleUrl)]
        public string Url { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }


        [Display(Name = LocalizationCaptions.StartDate)]
        public DateTime StartDateSearch { get; set; }
        public string StartDateSearchStr { get; set; }

        [Display(Name = LocalizationCaptions.EndDate)]
        public DateTime EndDateSearch { get; set; }
        public string EndDateSearchStr { get; set; }


        [Display(Name = LocalizationCaptions.Title)]
        public string TitleStr { get; set; }
    }
}
