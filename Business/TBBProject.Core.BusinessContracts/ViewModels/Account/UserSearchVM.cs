using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using TBBProject.Core.Common;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class UserSearchVM
    {
        [Display(Name = LocalizationCaptions.Name)]
        public string NameSearch { get; set; }
        [DataMember]
        [Display(Name = LocalizationCaptions.Surname)]
        public string SurnameSearch { get; set; }
    }
}
