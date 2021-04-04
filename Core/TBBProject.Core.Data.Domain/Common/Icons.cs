using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class Icons : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { set; get; }
    }
}
