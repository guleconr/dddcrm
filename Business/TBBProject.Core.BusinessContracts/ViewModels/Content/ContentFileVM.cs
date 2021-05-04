using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class ContentFileVM
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public long ContentLangId { get; set; }
        public ContentLangVM ContentLang { get; set; }
    }
}
