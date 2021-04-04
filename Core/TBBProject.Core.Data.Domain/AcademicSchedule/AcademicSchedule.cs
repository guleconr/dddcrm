using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class AcademicSchedule : IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Quota { get; set; }
        public List<AcademicScheduleLang> AcademicScheduleLang { get; set; }

    }
}
