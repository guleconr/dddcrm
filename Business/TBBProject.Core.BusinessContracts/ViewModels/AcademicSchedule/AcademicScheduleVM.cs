using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AcademicScheduleVM 
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.StartDate)]
        public DateTime StartDate { get; set; }
        [Display(Name = LocalizationCaptions.EndDate)]
        public DateTime EndDate { get; set; }
        [Display(Name = LocalizationCaptions.Quota)]
        public long Quota { get; set; }
        public List<AcademicScheduleLangVM> AcademicScheduleLang { get; set; }

        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Url)]
        public string Url { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }
    }
}
