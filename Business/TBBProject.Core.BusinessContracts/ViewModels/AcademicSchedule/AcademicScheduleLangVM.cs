using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AcademicScheduleLangVM 
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.Url)]
        public string Url { get; set; }


        public long AcademicScheduleId { get; set; }
        public AcademicScheduleVM AcademicSchedule { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        public long LanguageId { get; set; }
        public LanguageVM Language { get; set; }

    }
}
