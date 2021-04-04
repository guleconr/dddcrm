using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data.Domain;
using TBBProject.Core.DataLayer;

namespace TBBProject.Core.Business.Account
{

    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountDataLayer _accountDataLayer;
        private readonly IMapper _mapper;

        public AccountBusiness(IAccountDataLayer accountDataLayer, IMapper mapper)
        {
            _accountDataLayer = accountDataLayer;
            _mapper = mapper;

        }


        public UserVM GetUser(string username, string password)
        {
            var selectedUser = _accountDataLayer.GetUser(username, password);
            if (selectedUser != null)
            {

                var userDTO = _mapper.Map<UserVM>(selectedUser);
                List<Authority> authritys = new List<Authority>();
                foreach (var item in selectedUser.UserAuthority)
                {
                    authritys.Add(item.Authority);
                }
                foreach (var item in selectedUser.UserRole)
                {
                    foreach (var item2 in item.Role.RoleAuthority)
                    {
                        if (!authritys.Any(i => i.Id == item2.AuthorityId))
                            authritys.Add(item2.Authority);
                    }

                }
                foreach (var item in selectedUser.UserRole)
                {
                    foreach (var item2 in item.Role.RoleAuthority)
                    {
                        if (!authritys.Any(i => i.Id == item2.AuthorityId))
                            authritys.Add(item2.Authority);

                    }
                }
                userDTO.UserRoles = string.Join(",", selectedUser.UserRole.Select(i => i.Role.Name));
                userDTO.Authoritys = authritys.Distinct().Where(i => i.Status == Common.Enums.StatusEnum.Active).ToList();
                return userDTO;
            }
            return null;
        }
        public UserVM GetUserByEmail(string email)
        {
            var selectedUser = _accountDataLayer.GetUserByEmail(email);
            if (selectedUser != null)
            {

                var userDTO = _mapper.Map<UserVM>(selectedUser);
                List<Authority> authritys = new List<Authority>();
                foreach (var item in selectedUser.UserAuthority)
                {
                    authritys.Add(item.Authority);
                }
                foreach (var item in selectedUser.UserRole)
                {
                    foreach (var item2 in item.Role.RoleAuthority)
                    {
                        if (!authritys.Any(i => i.Id == item2.AuthorityId))
                            authritys.Add(item2.Authority);
                    }
                }

                userDTO.Authoritys = authritys.Distinct().ToList();
                userDTO.UserRoles = string.Join(",", selectedUser.UserRole.Select(i => i.Role.Name));
                return userDTO;
            }
            return null;
        }

        public async Task<List<UserVM>> GetUsersByMailTextAsync(string text)
        {
            var result = await _accountDataLayer.GetUsersByMailTextAsync(text);
            var users = _mapper.Map<List<UserVM>>(result);
            return users;
        }

        public async Task<List<UserVM>> ServerFiltering_UsersNameAsync(string name)
        {
            var result = await _accountDataLayer.ServerFiltering_UsersNameAsync(name);
            var users = _mapper.Map<List<UserVM>>(result);
            return users;
        }

        public async Task<List<UserVM>> ServerFiltering_UsersSurnameAsync(string surname)
        {
            var result = await _accountDataLayer.ServerFiltering_UsersSurnameAsync(surname);
            var users = _mapper.Map<List<UserVM>>(result);
            return users;
        }

        public async Task<List<UserVM>> ServerFiltering_UsersEmailAsync(string email)
        {
            var result = await _accountDataLayer.ServerFiltering_UsersEmailAsync(email);
            var users = _mapper.Map<List<UserVM>>(result);
            return users;
        }
    }
}
