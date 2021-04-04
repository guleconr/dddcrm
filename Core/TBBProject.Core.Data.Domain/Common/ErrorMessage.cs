using System.ComponentModel.DataAnnotations;

namespace TBBProject.Core.Data.Domain
{
    public class ErrorMessage : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Error {get;set;}
        public string Message { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }
    }
}