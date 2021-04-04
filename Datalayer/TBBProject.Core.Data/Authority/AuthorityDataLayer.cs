using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace TBBProject.Core.DataLayer
{
    public class AuthorityDataLayer : IAuthorityDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Users> _usersRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Authority> _authorityRepository;
        private readonly IRepository<RoleAuthority> _roleAuthorityRepository;
        public AuthorityDataLayer(IUnitOfWork uow)
        {
            _uow = uow;
            _roleRepository = _uow.Repository<Role>();
            _authorityRepository = _uow.Repository<Authority>();
            _roleAuthorityRepository = _uow.Repository<RoleAuthority>();
            _userRoleRepository = _uow.Repository<UserRole>();
            _usersRepository = _uow.Repository<Users>();
        }

        #region Role
        public IQueryable<Role> GetRoles()
        {
            var result = _roleRepository.TableNoTracking;
            return result;
        }
        public async Task<List<Role>> GetUserRolesAsync(long userId)
        {
            var result = await _userRoleRepository.TableNoTracking.Where(i => i.UserId == userId).ToListAsync();
            var roleresult = await _roleRepository.TableNoTracking.Where(i => result.Select(i => i.RoleId).Contains(i.Id)).ToListAsync();
            return roleresult;
        }

        public void CreateRoles(Role model)
        {
            _roleRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateRoles(Role model)
        {
            var result = _roleRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            result.Name = model.Name;
            _roleRepository.Update(result);
            _uow.SaveChanges();
        }
        public void DeleteRoles(Role model)
        {
            var result = _roleRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _roleRepository.Delete(result);
            _uow.SaveChanges();
        }
        public async Task UpdateUserRoleAsync(string roleList, long userId)
        {
            if(!string.IsNullOrEmpty(roleList))
            {
                var selectedList = _userRoleRepository.TableNoTracking.Where(i => i.UserId == userId).ToList();
                foreach (var item in selectedList)
                {
                    _userRoleRepository.Delete(item);
                }
                var auth = roleList.Split(',').ToList();
                foreach (var item in auth)
                {
                    var SelectedRoleId = Convert.ToInt64(item);
                    {
                        var selectedRole = await _roleRepository.TableNoTracking.Where(i => i.Id == SelectedRoleId).FirstOrDefaultAsync();
                        if (selectedRole != null)
                        {
                            UserRole insertModel = new UserRole
                            {
                                UserId = userId,
                                RoleId = SelectedRoleId
                            };
                            _userRoleRepository.Insert(insertModel);
                        }
                    }
                }
                await _uow.SaveChangesAsync();
            }
        }
        #endregion

        #region Authority
        public List<Authority> GetAuthorityList()
        {
            var result = _authorityRepository.TableNoTracking.ToList();
            return result;
        }
        public IQueryable<Authority> GetAuthoritys(long parentId)
        {
            if (parentId == 0)
            {
                var result = _authorityRepository.TableNoTracking.Where(i => i.ParentMenu == null);
                return result;
            }
            else
            {
                var result = _authorityRepository.TableNoTracking.Where(i => i.ParentMenu == parentId);
                return result;
            }
        }

        public void CreateAuthoritys(Authority model)
        {
            _authorityRepository.Insert(model);
            _uow.SaveChanges();
        }
        public Authority GetAuthorityById(long id)
        {
            var selected = _authorityRepository.TableNoTracking.Where(i => i.Id == id).FirstOrDefault();
            return selected;
        }
        public void UpdateAuthoritys(Authority model)
        {
            _authorityRepository.Update(model);
            _uow.SaveChanges();
        }
        public void DeleteAuthoritys(Authority model)
        {
            var result = _authorityRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            _authorityRepository.Delete(result);
            _uow.SaveChanges();
        }

        public List<RoleAuthority> GetRoleAuthorityList(long roleId)
        {
            var result = _roleAuthorityRepository.TableNoTracking.Where(i => i.RoleId == roleId).ToList();
            return result;
        }
        public async Task DeleteRoleAuthorityListAsync(long roleId)
        {
            var selectedList = _roleAuthorityRepository.TableNoTracking.Where(i => i.RoleId == roleId).ToList();
            foreach (var item in selectedList)
            {
                _roleAuthorityRepository.Delete(item);
            }
            await _uow.SaveChangesAsync();
        }

        public async Task AddRoleAuthorityAsync(string authList, long roleId)
        {
            List<long> resultList = new List<long>();
            var selectedList = _roleAuthorityRepository.TableNoTracking.Where(i => i.RoleId == roleId).ToList();
            foreach (var item in selectedList)
            {
                _roleAuthorityRepository.Delete(item);
            }
            var auth = authList.Split(',').ToList();
            foreach (var item in auth)
            {
                var SelectedAuthId = Convert.ToInt64(item);
                if (SelectedAuthId != 0)
                {
                    var selectedAuth = await _authorityRepository.TableNoTracking.Where(i => i.Id == SelectedAuthId).FirstOrDefaultAsync();
                    if (selectedAuth != null)
                    {
                        RoleAuthority insertModel;
                        insertModel = new RoleAuthority
                        {
                            AuthorityId = SelectedAuthId,
                            RoleId = roleId
                        };
                        _roleAuthorityRepository.Insert(insertModel);
                        resultList.Add(insertModel.AuthorityId);
                        if (selectedAuth != null)
                        {
                            do
                            {
                                selectedAuth = await _authorityRepository.TableNoTracking.Where(i => i.Id == selectedAuth.ParentMenu).FirstOrDefaultAsync();
                                if (selectedAuth != null)
                                {
                                    if (!resultList.Any(i => i == selectedAuth.Id))
                                    {
                                        insertModel = new RoleAuthority
                                        {
                                            AuthorityId = selectedAuth.Id,
                                            RoleId = roleId
                                        };
                                        _roleAuthorityRepository.Insert(insertModel);
                                        resultList.Add(insertModel.AuthorityId);
                                    }
                                }

                            } while (selectedAuth != null && selectedAuth.ParentMenu != null);
                        }
                    }
                }
            }
            await _uow.SaveChangesAsync();
        }
        #endregion

        #region Users
        public void CreateUsers(Users model)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = model.Id;
            model.Language = "tr-TR";
            model.IsFirstLogin = true;
            model.Status = Common.Enums.StatusEnum.Active;

                Role selectedRole = _roleRepository.TableNoTracking.Where(i => i.Name == "Users").FirstOrDefault();
                if (selectedRole != null)
                {
                    userRole.RoleId = selectedRole.Id;
                    _userRoleRepository.Insert(userRole);
                }
            
            _usersRepository.Insert(model);
            _uow.SaveChanges();
        }
        public void UpdateUsers(Users model)
        {
            Users selectedUser = _usersRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            UserRole userRole = new UserRole();
            if (selectedUser!=null)
            {
                userRole = _userRoleRepository.TableNoTracking.Where(i => i.UserId == model.Id).FirstOrDefault();
                if(userRole!=null)
                {
                    _userRoleRepository.Delete(userRole);
                }
            }
            UserRole userRoleNew = new UserRole();
            userRoleNew.UserId = model.Id;
            selectedUser.Name = model.Name;
            selectedUser.Surname = model.Surname;
            selectedUser.Email = model.Email;
           

                Role selectedRole = _roleRepository.TableNoTracking.Where(i => i.Name == "Users").FirstOrDefault();
                if (selectedRole != null)
                {
                    userRoleNew.RoleId = selectedRole.Id;
                    _userRoleRepository.Insert(userRoleNew);
                }
            
            _usersRepository.Update(selectedUser);
            _uow.SaveChanges();
        }
        public void DeleteUsers(Users model)
        {
            Users selectedUser = _usersRepository.TableNoTracking.Where(i => i.Id == model.Id).FirstOrDefault();
            UserRole userRole = new UserRole();
            if (selectedUser != null)
            {
                userRole = _userRoleRepository.TableNoTracking.Where(i => i.UserId == model.Id).FirstOrDefault();
                if (userRole != null)
                {
                    _userRoleRepository.Delete(userRole);
                }
            }
            selectedUser.Status = Common.Enums.StatusEnum.Pasive;
            _usersRepository.Update(selectedUser);
            _uow.SaveChanges();
        }
        #endregion
    }
}
