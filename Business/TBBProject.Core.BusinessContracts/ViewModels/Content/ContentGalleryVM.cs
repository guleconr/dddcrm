using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class ContentGalleryVM
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public long ContentLangId { get; set; }
        public ContentLangVM ContentLang { get; set; }
    }
}
