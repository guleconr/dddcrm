using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    public class UserVM
    {
        public long Id { get; set; }
        [Display(Name = LocalizationCaptions.Name)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = LocalizationCaptions.MaxMinLength)]
        public string Name { get; set; }
        [Display(Name = LocalizationCaptions.Surname)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = LocalizationCaptions.MaxMinLength)]
        public string Surname { get; set; }
        [Display(Name = LocalizationCaptions.Email)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [EmailAddress(ErrorMessage = LocalizationCaptions.Email)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = LocalizationCaptions.EmailFormatError)]
        public string Email { get; set; }
        [Display(Name = LocalizationCaptions.Password)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public bool IsFirstLogin { get; set; }
        public int IncorrectLoginCount { get; set; }
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Password { get; set; }
        [Display(Name = LocalizationCaptions.Language)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Language { get; set; }

        public StatusEnum Status { get; set; }
        public string UserRoles { get; set; }
        public long UserRoleId { get; set; }

        [Display(Name = LocalizationCaptions.Roles)]
        public List<RoleVM> UserRolesList { get; set; }
        [DataMember]
        public List<Authority> Authoritys { get; set; }
        [Display(Name = LocalizationCaptions.UserRole)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public long RoleId { get; set; }
    }

    public class UserLoginVM
    {
        [Display(Name = LocalizationCaptions.Email)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [EmailAddress(ErrorMessage = LocalizationCaptions.Email)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = LocalizationCaptions.EmailFormatError)]
        public string Email { get; set; }
        [Display(Name = LocalizationCaptions.Password)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Password { get; set; }
    }

    public class ChangePasswordVM
    {
        [Display(Name = LocalizationCaptions.Email)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        [EmailAddress(ErrorMessage = LocalizationCaptions.Email)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = LocalizationCaptions.EmailFormatError)]
        public string Email { get; set; }
        [Display(Name = LocalizationCaptions.Password)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string Password { get; set; }
        [Display(Name = LocalizationCaptions.Password)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string NewPassword { get; set; }
        [Display(Name = LocalizationCaptions.Password)]
        [Required(ErrorMessage = LocalizationCaptions.Required)]
        public string RepeatNewPassword { get; set; }
    }
}
