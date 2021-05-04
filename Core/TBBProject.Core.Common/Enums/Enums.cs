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

    public enum DynamicMenuTypeEnum
    {
       
        [Display(Name = LocalizationCaptions.No)]
        Text = 0,
        [Display(Name = LocalizationCaptions.Yes)]
        Radio = 1,
        [Display(Name = LocalizationCaptions.Yes)]
        Dropdown = 2,
        [Display(Name = LocalizationCaptions.Yes)]
        Datepicker = 3,
        [Display(Name = LocalizationCaptions.Yes)]
        TextArea = 4,
    };

    public enum MunicipalityEnum
    {
        [Display(Name = LocalizationCaptions.BigCity)]
        BigCity = 0,
        [Display(Name = LocalizationCaptions.BigDistrict)]
        BigDistrict = 1,
        [Display(Name = LocalizationCaptions.City)]
        City = 2,
        [Display(Name = LocalizationCaptions.District)]
        District = 3
    };

    public enum MunicipalityCityEnum
    {
        [Display(Name = LocalizationCaptions.BigCity)]
        BigCity = 0,
        [Display(Name = LocalizationCaptions.City)]
        City = 2,
    };

}
