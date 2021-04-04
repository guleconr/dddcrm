using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Data.Domain
{
    public class NewsGallery : IEntity
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public long NewsLangId { get; set; }
        public NewsLang NewsLang { get; set; }

    }
}
