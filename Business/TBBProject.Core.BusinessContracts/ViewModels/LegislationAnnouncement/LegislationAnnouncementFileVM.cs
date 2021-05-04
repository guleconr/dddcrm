using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class LegislationAnnouncementFileVM
    {
        [Key] 
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public LegislationAnnouncementLangVM LegislationAnnouncementLang { get; set; }
    }
}
