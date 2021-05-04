using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataContracts
{
    public interface IAuthorityDataLayer
    {
        #region Role
        IQueryable<Role> GetRoles();
        Task<List<Role>> GetUserRolesAsync(long userId);
        void CreateRoles(Role model);

        void UpdateRoles(Role model);

        void DeleteRoles(Role model);
        Task UpdateUserRoleAsync(string roleList, long userId);
        #endregion

        #region Authority
        List<Authority> GetAuthorityList();

        IQueryable<Authority> GetAuthoritys(long parentId);

        void CreateAuthoritys(Authority model);

        void UpdateAuthoritys(Authority model);

        void DeleteAuthoritys(Authority model);
        Authority GetAuthorityById(long id);

        List<RoleAuthority> GetRoleAuthorityList(long roleId);

        Task DeleteRoleAuthorityListAsync(long roleId);

        Task AddRoleAuthorityAsync(string authList,long roleId);

        #endregion

        #region Users
        void CreateUsers(Users model,long roleId);

        void UpdateUsers(Users model, long roleId);

        void DeleteUsers(Users model);
        #endregion
    }
}
