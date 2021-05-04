using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class AuthorityVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Name)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Name { get; set; }
        public long? ParentMenu { get; set; }
        [Display(Name = LocalizationCaptions.Text)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Text { get; set; }
        [Display(Name = LocalizationCaptions.Icon)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Icon { get; set; }
        [Display(Name = LocalizationCaptions.Url)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Url { get; set; }
        [Display(Name = LocalizationCaptions.AuthorityType)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public AuthorityTypeEnum AuthorityType { get; set; }
        [Display(Name = LocalizationCaptions.MenuOrder)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public Nullable<int> MenuOrder { get; set; }
        [Display(Name = LocalizationCaptions.IsMenu)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public YesNoEnum IsMenu { get; set; }
        [Display(Name = LocalizationCaptions.Status)]
        public StatusEnum Status { get; set; }
        [Display(Name = LocalizationCaptions.Title)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Title { get; set; }
        [Display(Name = LocalizationCaptions.BreadCrumb)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string BreadCrumb { get; set; }
        public bool hasChildren { get; set; }
        public List<AuthorityVM> ChildAuthority { get; set; } = new List<AuthorityVM>();

    }

    public class AuthorityTreeVM
    {
        public long id { get; set; }
        public string Name { get; set; }
        public bool hasChildren { get; set; }
        public bool ischecked { get; set; }
        public List<AuthorityTreeVM> items { get; set; } = new List<AuthorityTreeVM>();
        public int Order { get; set; }

    }
}
