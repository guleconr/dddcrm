using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class NewsGalleryVM
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public long NewsLangId { get; set; }
        public NewsLangVM NewsLangVM { get; set; }
    }
}
