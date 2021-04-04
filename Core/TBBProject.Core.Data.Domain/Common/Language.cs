using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TBBProject.Core.Data.Domain
{
    public class Language : IEntity
    {
        [Key]
        public long Id { get; set; }
        public virtual List<Resource> Resources { set; get; }
        public string CultureName { set; get; }
        public string Name { set; get; }
        public string Country { set; get; }
        public string Region { set; get; }
        public bool IsDefault { set; get; }     
    }
}