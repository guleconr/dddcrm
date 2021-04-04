using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBBProject.Core.Common.Enums;
using TBBProject.Core.Data.Domain;

namespace TBBProject.Core.DataLayer
{
    public interface IAccountDataLayer
    {
        Users GetUser(string username, string password);
        Users GetUserByEmail(string email);
        Task<List<Users>> GetUsersByMailTextAsync(string text);
        Task<List<Users>> ServerFiltering_UsersNameAsync(string name);
        Task<List<Users>> ServerFiltering_UsersSurnameAsync(string surname);
        Task<List<Users>> ServerFiltering_UsersEmailAsync(string surname);
    }
}
