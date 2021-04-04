using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class VideoGalleryLang : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }


        public long VideoGalleryId { get; set; }
        public VideoGallery VideoGallery { get; set; }
        public long LanguageId { get; set; }
        public Language Language { get; set; }

    }
}
