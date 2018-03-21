using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Account ;


namespace DeluxeOM.Repository
{
    public interface IAccountRepository
    {
        UserModel GetUserByUserName(string email);
        void IncrementLoginAttempts(string userName);

        void SetLastLoginInfo(int userId);

        void ExpiredPassword(PasswordReset model);

        bool ForgotPassword(PasswordResetModel prEntity);

        UserModel VerifyPasswordReset(string token);

        bool ResetPassword(PasswordReset passwordReset);
    }
}
