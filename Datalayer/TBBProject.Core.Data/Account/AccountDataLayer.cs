using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Common.Extensions;
using TBBProject.Core.Data;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataLayer.Account
{

    public class AccountDataLayer : IAccountDataLayer
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Users> _userRepository;
        private readonly EncryptionService _encryptionService;

        public AccountDataLayer(IUnitOfWork uow, EncryptionService encryptionService)
        {
            _uow = uow;
            _userRepository = _uow.Repository<Users>();
            _encryptionService = encryptionService;

        }


        public Users GetUser(string username, string password)
        {
            var enc = _encryptionService.Encrypt(password);
            var selectedUser = _userRepository.TableNoTracking.Include(i => i.UserAuthority).ThenInclude(i => i.Authority).Include(i => i.UserRole).
                ThenInclude(i => i.Role).ThenInclude(i => i.RoleAuthority).ThenInclude(i => i.Authority).Where(i => i.Email.ToLower() == username.ToLower() && i.Password == enc && i.Status == StatusEnum.Active).FirstOrDefault();
            return selectedUser;
        }

        public Users GetUserByEmail(string email)
        {
            var selectedUser = _userRepository.TableNoTracking.Include(i => i.UserAuthority).ThenInclude(i => i.Authority).Include(i => i.UserRole).
                ThenInclude(i => i.Role).ThenInclude(i => i.RoleAuthority).ThenInclude(i => i.Authority).Where(i => i.Email.ToLower() == email.ToLower()).FirstOrDefault();
            return selectedUser;
        }
        public async Task<List<Users>> GetUsersByMailTextAsync(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                List<Users> result = await _userRepository.TableNoTracking.Where(i => i.Email.ToLower().Contains(text.ToLower()) && i.Status == Common.Enums.StatusEnum.Active).ToListAsync();
                return result;

            }
            else
            {
                List<Users> result = await _userRepository.TableNoTracking.ToListAsync();
                return result;
            }
        }

        public async Task<List<Users>> ServerFiltering_UsersNameAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                List<Users> result = await _userRepository.TableNoTracking.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<List<Users>> ServerFiltering_UsersSurnameAsync(string surname)
        {
            if (!string.IsNullOrEmpty(surname))
            {
                List<Users> result = await _userRepository.TableNoTracking.Where(i => i.Surname.ToLower().Contains(surname.ToLower())).ToListAsync();
                return result;
            }
            return null;
        }
        public async Task<List<Users>> ServerFiltering_UsersEmailAsync(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                List<Users> result = await _userRepository.TableNoTracking.Where(i => i.Email.ToLower().Contains(email.ToLower())).ToListAsync();
                return result;
            }
            return null;
        }

    }
}
