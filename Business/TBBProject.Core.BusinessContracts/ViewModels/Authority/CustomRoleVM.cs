using System;
using System.Collections.Generic;
using System.Text;

namespace TBBProject.Core.BusinessContracts.ViewModels
{
    
    public class CustomRoleVM
    {
        public List<RoleVM> SelectedRoles { get; set; }
        public List<RoleVM> SelectableRoles { get; set; }
    }
}
