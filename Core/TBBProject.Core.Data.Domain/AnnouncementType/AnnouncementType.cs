using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class AnnouncementType : IEntity
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
