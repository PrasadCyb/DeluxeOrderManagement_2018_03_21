using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Models.Account;

namespace DeluxeOM.Repository
{
    public interface IUserRepository
    {
        List<UserModel> GetAll();
        UserModel GetById(int id);
        UserModel GetByEmail(string email);
        void Save(UserModel user);
        void Update(UserModel user);
        void Delete(int id);

        void ChangePassword(UserPasswordModel upEntity);

        List<RoleModel> GetRoles();
    }
}
