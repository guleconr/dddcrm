using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class LanguageVM
    {
        public long Id { get; set; }
        public string CultureName { set; get; }
        [Display(Name = LocalizationCaptions.Language)]
        public string Name { set; get; }
        public string Country { set; get; }
        public string Region { set; get; }
        public bool IsDefault { set; get; }
        public virtual List<ResourcesVM> Resources { set; get; }
    }
}
