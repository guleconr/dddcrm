using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class ResourcesVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Name { set; get; }
        [Display(Name = LocalizationCaptions.Value)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Value { set; get; }
        [UIHint("LanguageDropDownEditor")]
        [Display(Name = LocalizationCaptions.Language)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public long LanguageId { set; get; }
    }
}
