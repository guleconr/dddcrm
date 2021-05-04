using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common;
using TBBProject.Core.Data.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TBBProject.Core.DataContracts;

namespace TBBProject.Core.Business
{
    public class AuthorityBusiness : IAuthorityBusiness
    {
        private readonly IAuthorityDataLayer _authorityDataLayer;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AuthorityBusiness> _localizer;
        public AuthorityBusiness(IAuthorityDataLayer authorityDataLayer, IMapper mapper, IStringLocalizer<AuthorityBusiness> localizer)
        {
            _authorityDataLayer = authorityDataLayer;
            _mapper = mapper;
            _localizer = localizer;
        }

        #region Roles
        public DataSourceResult GetRoles([DataSourceRequest] DataSourceRequest request)
        {
            var resources = _authorityDataLayer.GetRoles().OrderBy(i => i.Name).ToDataSourceResult(request);
            resources.Data = _mapper.Map<List<RoleVM>>(( resources.Data ));
            return resources;
        }
        public List<RoleVM> GetRoles()
        {
            var resources = _authorityDataLayer.GetRoles().OrderBy(i => i.Name);
            var result = _mapper.Map<List<RoleVM>>(( resources ));
            return result;
        }
        public async Task<CustomRoleVM> GetUserRolesAsync(long userId)
        {
            CustomRoleVM model = new CustomRoleVM();
            var roles = await _authorityDataLayer.GetUserRolesAsync(userId);
            var result = _mapper.Map<List<RoleVM>>(( roles ));
            var allRoles = await _authorityDataLayer.GetRoles().OrderBy(i => i.Name).ToListAsync();
            model.SelectedRoles = result;
            var selectableroles = allRoles.Where(i => !result.Select(i => i.Id).Contains(i.Id)).ToList();
            model.SelectableRoles = _mapper.Map<List<RoleVM>>(( selectableroles ));
            return model;
        }
        public void CreateRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources)
        {
            var eResources = _mapper.Map<List<Role>>(( resources.Where(i => i.RoleAuthority == null).ToList() ));
            foreach (var item in eResources)
            {
                _authorityDataLayer.CreateRoles(item);
            }
        }
        public void UpdateRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources)
        {
            var eResources = _mapper.Map<List<Role>>(( resources ));
            foreach (var item in eResources)
            {
                _authorityDataLayer.UpdateRoles(item);
            }
        }
        public void DeleteRoles([DataSourceRequest] DataSourceRequest request, IEnumerable<RoleVM> resources)
        {
            var eResources = _mapper.Map<List<Role>>(( resources ));
            foreach (var item in eResources)
            {
                _authorityDataLayer.DeleteRoles(item);
            }
        }


        public async Task<FormResultModel> UpdateUserRoleAsync(string selectedIds, long selectedUserId)
        {
            try
            {
                await _authorityDataLayer.UpdateUserRoleAsync(selectedIds, selectedUserId);
                FormResultModel result = new FormResultModel
                {
                    IsSuccess = true,
                    ResultText = _localizer[LocalizationCaptions.UserRoleSuccess].Value
                };
                return result;
            }
            catch (Exception e)
            {
                FormResultModel result = new FormResultModel
                {
                    IsSuccess = false,
                    ResultText = _localizer[LocalizationCaptions.UserRoleError].Value
                };
                return result;
            }
        }
        #endregion

        #region AuthorityTree
        public List<AuthorityTreeVM> GetAuthorityTree(long? id)
        {
            List<AuthorityTreeVM> result = new List<AuthorityTreeVM>();

            if (!id.HasValue)
            {
                AuthorityTreeVM first = new AuthorityTreeVM
                {
                    id = 0,
                    hasChildren = true,
                    Name = _localizer["Main Authority"],
                    ischecked = false
                };
                result.Add(first);
            }
            else
            {
                var authList = _mapper.Map<List<AuthorityVM>>(( _authorityDataLayer.GetAuthorityList().OrderBy(i => i.ParentMenu).ToList() ));
                if (id == 0)
                {
                    result.AddRange(authList.Where(i => i.ParentMenu == null).Select(i => new AuthorityTreeVM
                    {

                        id = i.Id,
                        Name = _localizer[i.Name],
                        ischecked = true,
                        Order = i.MenuOrder.HasValue ? i.MenuOrder.Value : 9
                    }));
                }
                else
                {
                    result.AddRange(authList.Where(i => i.ParentMenu == id).Select(i => new AuthorityTreeVM
                    {

                        id = i.Id,
                        Name = _localizer[i.Name],
                        ischecked = true,
                        Order=i.MenuOrder.HasValue?i.MenuOrder.Value:9
                    }));
                }
                foreach (var item in result)
                {
                    item.items = GetNestedAuthorityList2(item, authList).OrderBy(i=>i.Order).ToList();
                    item.hasChildren = item.items.Count > 0 ? true : false;
                }
            }

            return result;
        }
        public List<AuthorityTreeVM> GetRoleAuthorityTree(long? id, long roleId)
        {
            var roleAutList = _authorityDataLayer.GetRoleAuthorityList(roleId);
            List<AuthorityTreeVM> result = new List<AuthorityTreeVM>();

            if (!id.HasValue)
            {
                AuthorityTreeVM first = new AuthorityTreeVM
                {
                    id = 0,
                    hasChildren = true,
                    Name = _localizer["Main Authority"],
                    ischecked = false
                };
                result.Add(first);
            }
            else
            {
                var authList = _mapper.Map<List<AuthorityVM>>(( _authorityDataLayer.GetAuthorityList().OrderBy(i => i.ParentMenu).ToList() ));
                if (id == 0)
                {
                    result.AddRange(authList.Where(i => i.ParentMenu == null).Select(i => new AuthorityTreeVM
                    {

                        id = i.Id,
                        Name = _localizer[i.Name],
                        ischecked = roleAutList.Any(k => k.AuthorityId == i.Id)
                    }));
                }
                else
                {
                    result.AddRange(authList.Where(i => i.ParentMenu == id).Select(i => new AuthorityTreeVM
                    {

                        id = i.Id,
                        Name = _localizer[i.Name],
                        ischecked = roleAutList.Any(k => k.AuthorityId == i.Id)
                    }));
                }
                foreach (var item in result)
                {
                    item.items = GetNestedAuthorityList2(item, authList);
                    item.hasChildren = item.items.Count > 0 ? true : false;
                }
            }

            return result;
        }
        public List<AuthorityTreeVM> GetNestedAuthorityList2(AuthorityTreeVM selected, List<AuthorityVM> allAut)
        {
            List<AuthorityTreeVM> result = new List<AuthorityTreeVM>();
            result.AddRange(allAut.Where(i => i.ParentMenu == selected.id).Select(i => new AuthorityTreeVM
            {

                id = i.Id,
                Name = _localizer[i.Name],
                Order = i.MenuOrder.HasValue ? i.MenuOrder.Value : 9
            }));
            foreach (var item in result)
            {

                item.items = GetNestedAuthorityList2(item, allAut);
                item.hasChildren = item.items.Count > 0 ? true : false;
            }
            return result;
        }

        public DataSourceResult GetAuthoritys([DataSourceRequest] DataSourceRequest request, long parentId)
        {
            var resources = _authorityDataLayer.GetAuthoritys(parentId).OrderBy(i => i.Name).ToDataSourceResult(request);
            resources.Data = _mapper.Map<List<AuthorityVM>>(( resources.Data ));
            return resources;
        }
        public void CreateAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys)
        {
            var eauthoritys = _mapper.Map<List<Authority>>(( authoritys ));
            foreach (var item in eauthoritys)
            {
                _authorityDataLayer.CreateAuthoritys(item);
            }
        }
        public void UpdateAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys)
        {
            foreach (var item in authoritys)
            {
                var maniModel = _authorityDataLayer.GetAuthorityById(item.Id);
                var eauthoritys = _mapper.Map<AuthorityVM, Authority>(item, maniModel);
                _authorityDataLayer.UpdateAuthoritys(eauthoritys);
            }
        }
        public void DeleteAuthoritys([DataSourceRequest] DataSourceRequest request, IEnumerable<AuthorityVM> authoritys)
        {
            var eauthoritys = _mapper.Map<List<Authority>>(( authoritys ));
            foreach (var item in eauthoritys)
            {
                _authorityDataLayer.DeleteAuthoritys(item);
            }
        }

        public async Task<FormResultModel> UpdateRoleAuthoritysAsync(string selectedIds, long selectedRoleId)
        {
            try
            {
                await _authorityDataLayer.AddRoleAuthorityAsync(selectedIds, selectedRoleId);
                FormResultModel result = new FormResultModel
                {
                    IsSuccess = true,
                    ResultText = _localizer[LocalizationCaptions.AuthorizeSuccess].Value
                };
                return result;
            }
            catch (Exception e)
            {
                FormResultModel result = new FormResultModel
                {
                    IsSuccess = false,
                    ResultText = _localizer[LocalizationCaptions.AuthorizeError].Value
                };
                return result;
            }
        }
        #endregion

        #region Users
        public void CreateUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users)
        {
            foreach (var item in users)
            {
                var eUsers = _mapper.Map<Users>(( item ));
                _authorityDataLayer.CreateUsers(eUsers, item.RoleId);
            }
        }
        public void UpdateUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users)
        {
            foreach (var item in users)
            {
                var eUsers = _mapper.Map<Users>(( item ));
                _authorityDataLayer.UpdateUsers(eUsers, item.RoleId);
            }
        }
        public void DeleteUsers([DataSourceRequest] DataSourceRequest request, IEnumerable<UserVM> users)
        {
            var eUsers = _mapper.Map<List<Users>>(( users ));
            foreach (var item in eUsers)
            {
                _authorityDataLayer.DeleteUsers(item);
            }
        }
        #endregion
    }
}
