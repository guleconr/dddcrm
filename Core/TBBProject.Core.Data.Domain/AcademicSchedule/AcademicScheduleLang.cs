using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class AcademicScheduleLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }


        public long AcademicScheduleId { get; set; }
        public AcademicSchedule AcademicSchedule { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }

    }
}
