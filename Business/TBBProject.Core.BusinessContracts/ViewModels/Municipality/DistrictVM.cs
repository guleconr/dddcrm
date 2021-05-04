using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
   public class DistrictVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Name)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Name { get; set; }
        [Display(Name = LocalizationCaptions.Phone)]
        [RegularExpression("^[0-9]*$", ErrorMessage = LocalizationCaptions.OnlyNumber)]
        public string Phone { get; set; }
        [Display(Name = LocalizationCaptions.Email)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = LocalizationCaptions.EmailFormatError)]
        public string Email { get; set; }
        [Display(Name = LocalizationCaptions.Population)]
        public decimal Population { get; set; }
        [Display(Name = LocalizationCaptions.MunicipalityType)]
        public MunicipalityEnum MunicipalityType { get; set; }
        public long MunicipalityId { get; set; }
        public MunicipalityVM Municipality { get; set; }
    }
}
