using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
   public class District : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Population { get; set; }
        public MunicipalityEnum MunicipalityType { get; set; }
        public long MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}
