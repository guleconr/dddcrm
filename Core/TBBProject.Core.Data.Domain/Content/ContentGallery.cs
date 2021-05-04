using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TBBProject.Core.Data.Domain
{
    public class ContentGallery : IEntity
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public long ContentLangId { get; set; }
        public ContentLang ContentLang { get; set; }

    }
}
