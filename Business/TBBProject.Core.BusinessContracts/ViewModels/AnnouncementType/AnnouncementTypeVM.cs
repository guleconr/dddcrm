using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AnnouncementTypeVM 
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Name)]
        public string Name { get; set; }
    }
}
