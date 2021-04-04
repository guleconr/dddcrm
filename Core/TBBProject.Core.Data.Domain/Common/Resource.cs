using System.ComponentModel.DataAnnotations;

namespace TBBProject.Core.Data.Domain
{
    public class Resource : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { set; get; }
        public string Value { set; get; }
        public virtual Language Language { set; get; } 
        public long LanguageId { set; get; }
    }
}