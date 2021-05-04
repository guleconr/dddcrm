using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
   public class ContentLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public List<ContentFile> ContentFile { get; set; }
        public List<ContentGallery> ContentGallery { get; set; }
        public long ContentId { get; set; }
        public Content ContentObj { get; set; }
        public long UserId { get; set; }
        public Users User { get; set; }
    }
}
