using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using TBBProject.Core.BusinessContracts.ViewModels;
using TBBProject.Core.Common.Enums;

namespace TBBProject.Core.BusinessContracts
{
    public interface IAccountBusiness
    {
        UserVM GetUser(string username, string password);
        UserVM GetUserByEmail(string email);
        Task<List<UserVM>> GetUsersByMailTextAsync(string text);
        Task<List<UserVM>> ServerFiltering_UsersNameAsync(string name);
        Task<List<UserVM>> ServerFiltering_UsersSurnameAsync(string surname);
        Task<List<UserVM>> ServerFiltering_UsersEmailAsync(string email);

    }
}
