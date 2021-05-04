using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class NewsLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string HeadLine { get; set; }
        public string Content { get; set; }
        public byte[] SliderImage { get; set; }
        public string SliderImageName { get; set; }
        public string NewsImageName { get; set; }
        public byte[] NewsImage { get; set; }
        public List<NewsGallery> NewsGallery { get; set; }
        public long NewsId { get; set; }
        public News News { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }
        public long UserId { get; set; }
        public Users User { get; set; }

    }
}
