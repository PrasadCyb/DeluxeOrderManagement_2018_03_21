using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Account;
using DeluxeOM.Models;

namespace DeluxeOM.Services
{
    public interface IManageService
    {
        List<User> GetAllUsers();

        User GetUserById(int userId);

        SaveResult SaveUser(User user);

        SaveResult DeleteUser(int userId);

        SaveResult UpdateUser(User user);

        SaveResult ChangePassword(PasswordReset model);

        SaveResult  AssignRoles(int userId, List<RoleMembership> roles);

        SaveResult RemoveRole(int userId, int roleId);

        List<RoleMembership> GetRoles();
        bool EmailExists(string email, int? userId = null);

        
    }
}
