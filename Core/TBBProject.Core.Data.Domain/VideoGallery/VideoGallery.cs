using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.Data.Domain
{
    public class VideoGallery : IEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<VideoGalleryLang> VideoGalleryLang { get; set; }

    }
}
