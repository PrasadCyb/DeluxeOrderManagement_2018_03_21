using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Account;
using DeluxeOM.Models;

namespace DeluxeOM.Services
{
    public interface IAccountService
    {
        AuthResult Authenticate(User user);

        string[] GetRoles(string userName);

        SaveResult ExpiredPassword(PasswordReset model);

        SaveResult ForgotPassword(PasswordReset model);

        User VerifyPasswordReset(string token);

        SaveResult  ResetPassword(PasswordReset passwordReset);

    }
}
