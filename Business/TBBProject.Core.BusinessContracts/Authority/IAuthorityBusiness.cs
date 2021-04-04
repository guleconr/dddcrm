using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.BusinessContracts.ViewModels;
using Kendo.Mvc.UI;

namespace TBBProject.Core.BusinessContracts
{
    public interface IAuthorityBusiness
    {
        DataSourceResult GetRoles([DataSourceRequest] DataSourceRequest request);
        List<RoleVM> GetRoles();
        Task<CustomRoleVM> GetUserRolesAsync(long userId);
        void CreateRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources);
        void UpdateRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources);
        void DeleteRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources);
        List<AuthorityTreeVM> GetAuthorityTree(long? id);
        List<AuthorityTreeVM> GetRoleAuthorityTree(long? id, long roleId);
        DataSourceResult GetAuthoritys([DataSourceRequest] DataSourceRequest request,long parentId);
        void CreateAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys);
        void UpdateAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys);
        void DeleteAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys);


        Task<FormResultModel> UpdateRoleAuthoritysAsync(string selectedIds, long selectedRoleId);
        Task<FormResultModel> UpdateUserRoleAsync(string selectedIds, long selectedUserId);


        void CreateUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users);
        void UpdateUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users);
        void DeleteUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users);

    }
}
