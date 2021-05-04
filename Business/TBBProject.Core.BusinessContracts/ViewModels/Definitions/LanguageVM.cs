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
        [Display(Name = LocalizationCaptions.CultureName)]
        public string CultureName { set; get; }
        [Display(Name = LocalizationCaptions.Language)]
        public string Name { set; get; }
        [Display(Name = LocalizationCaptions.Country)]
        public string Country { set; get; }
        [Display(Name = LocalizationCaptions.Region)]
        public string Region { set; get; }
        [Display(Name = LocalizationCaptions.IsDefault)]
        public bool IsDefault { set; get; }
        public virtual List<ResourcesVM> Resources { set; get; }
    }
}
