using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class RoleVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.ReleaseDate)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Name { get; set; }
        [DataMember]
        virtual public ICollection<RoleAuthority> RoleAuthority { get; set; }
    }
}
