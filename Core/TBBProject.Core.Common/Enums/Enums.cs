using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBBProject.Core.Common.Enums
{
  

    public enum YesNoEnum
    {
        [Display(Name = LocalizationCaptions.Yes)]
        Yes = 1,
        [Display(Name = LocalizationCaptions.No)]
        No = 0
    };

    public enum AuthorityTypeEnum
    {
        [Display(Name = LocalizationCaptions.Page)]
        Page = 1,
        [Display(Name = LocalizationCaptions.SubAuthority)]
        SubAuthority = 0
    };

    public enum StatusEnum
    {
        [Display(Name = LocalizationCaptions.Active)]
        Active = 1,
        [Display(Name = LocalizationCaptions.Pasive)]
        Pasive = 0
    };

    public enum ApprovalStatus
    {
        [Display(Name = LocalizationCaptions.Approval)]
        Approval = 1,
        [Display(Name = LocalizationCaptions.Waiting)]
        Waiting = 0
    };

}
