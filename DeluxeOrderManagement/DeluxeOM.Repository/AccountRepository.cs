using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Account;
using DeluxeOM.Repository;

namespace DeluxeOM.Repository
{
    /// <summary>
    /// Methods for password management.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {
            
        }
        /// <summary>
        /// Search user by Email Id
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        public UserModel GetUserByUserName(string email)
        {
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var userety = _acoubntContext.UserModels
                    .Include("UserPasswords")
                    .Include("UserRoles")
                    .Include("UserRoles.Role.RolePrivs.Privilege")
                    .FirstOrDefault(e => e.Email == email && e.Active);
                return userety;
            }
            
        }

        /// <summary>
        /// Increment login attempts of user 
        /// </summary>
        /// <param name="userName"></param>
        public void IncrementLoginAttempts(string userName)
        {
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var ety = _acoubntContext.UserModels.FirstOrDefault(e => e.UserName == userName);
                if (ety != null)
                {
                    ety.LoginAttempts += 1;
                    _acoubntContext.Entry(ety).State = System.Data.Entity.EntityState.Modified;
                    _acoubntContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Update last login info of user
        /// </summary>
        /// <param name="userId">Current userId</param>
        public void SetLastLoginInfo(int userId)
        {
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var ety = _acoubntContext.UserModels.FirstOrDefault(e => e.UserId == userId);
                ety.LoginAttempts = 0;
                ety.LastLogin = DateTime.UtcNow;

                _acoubntContext.Entry(ety).State = System.Data.Entity.EntityState.Modified;

                _acoubntContext.SaveChanges();
            }
        }

        /// <summary>
        /// Create new password
        /// </summary>
        /// <param name="passwordReset">Contains password details</param>
        public void ExpiredPassword(PasswordReset passwordReset)
        {
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var uety = _acoubntContext.UserModels.FirstOrDefault(e => e.Email == passwordReset.Email);
                var upEty = new UserPasswordModel()
                {
                    UserId = uety.UserId,
                    Password = passwordReset.Password,
                    PasswordDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow
                };
                _acoubntContext.UserPasswordModels.Add(upEty);
                _acoubntContext.Entry(upEty).State = System.Data.Entity.EntityState.Added;

                uety.LoginAttempts = 0;
                _acoubntContext.Entry(uety).State = System.Data.Entity.EntityState.Modified;

                _acoubntContext.SaveChanges();
            }
        }

        /// <summary>
        /// Insert Guid and email with current time into PasswordReset Table
        /// </summary>
        /// <param name="prEntity"></param>
        /// <returns>Ture on successful insert</returns>
        public bool ForgotPassword(PasswordResetModel prEntity)
        {
            bool bresult = false;
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var ety = _acoubntContext.UserModels.FirstOrDefault(e => e.Email == prEntity.Email && e.Active == true);
                if (ety != null)
                {
                    //Max
                    ety.LoginAttempts = 99;
                    _acoubntContext.Entry(ety).State = System.Data.Entity.EntityState.Modified;

                    prEntity.UserId = ety.UserId;
                    _acoubntContext.PasswordResetModels.Add(prEntity);
                    _acoubntContext.Entry(prEntity).State = System.Data.Entity.EntityState.Added;

                    _acoubntContext.SaveChanges();

                    prEntity.UserId = ety.UserId;
                    prEntity.Email = ety.Email;

                    bresult = true;
                }
            }
            
            return bresult;
        }

        /// <summary>
        /// Check password reset token is expired or not
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserModel VerifyPasswordReset(string token)
        {
            UserModel userEty = null;
            DateTime utctime = DateTime.UtcNow;
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var prety = _acoubntContext.PasswordResetModels
                    .Include("User")
                    .Include("User.UserPasswords")
                    .Include("User.UserRoles")
                    .Include("User.UserRoles.Role")
                    .Include("User.UserRoles.Role.RolePrivs")
                    .Include("User.UserRoles.Role.RolePrivs.Privilege")
                    .FirstOrDefault(x => x.Token == token && x.ExpireDateTime >= utctime && (x.User != null && x.User.Active == true));

                
                userEty = prety != null ? prety.User : null;
                //var user = (from pr in _acoubntContext.PasswordResetEntities
                //            join u in _acoubntContext.UserEntities on pr.UserId equals u.UserId
                //            where pr.Token == token && pr.ExpireDateTime >= utctime && u.Active
                //            select u);
            }
            return userEty;
        }

        /// <summary>
        /// Create new password
        /// </summary>
        /// <param name="passwordReset">Contains password details</param>
        /// <returns>True if password reset successfully else False</returns>
        public bool ResetPassword(PasswordReset passwordReset)
        {
            DateTime utctime = DateTime.UtcNow;
            UserModel userEty = null;
            bool bresult = false;
            using (var _acoubntContext = new DeluxeOrderManagementEntities())
            {
                var prety = _acoubntContext.PasswordResetModels
                    .Include("User")
                    .FirstOrDefault(x => x.Email == passwordReset.Email && x.Token == passwordReset.Token
                        && x.ExpireDateTime >= utctime && (x.User != null && x.User.Active == true));

                //var user = (from pr in _acoubntContext.PasswordResetEntities
                //            join u in _acoubntContext.UserEntities on pr.UserId equals u.UserId
                //            where pr.Email == passwordReset.Email && pr.Token == passwordReset.Token && pr.ExpireDateTime >= utctime && u.Active
                //            select u).FirstOrDefault();
                if (prety != null)
                {
                    userEty = prety.User;
                }
                if (userEty != null)
                {
                    UserPasswordModel upEty = new UserPasswordModel()
                    {
                        UserId = userEty.UserId,
                        Password = passwordReset.Password,
                        PasswordDate = utctime,
                        CreatedDate = utctime
                    };
                    _acoubntContext.UserPasswordModels.Add(upEty);
                    _acoubntContext.Entry(upEty).State = System.Data.Entity.EntityState.Added;

                    userEty.LoginAttempts = 0;
                    _acoubntContext.Entry(userEty).State = System.Data.Entity.EntityState.Modified;

                    //var pwdreset = (from pr in _acoubntContext.PasswordResetEntities
                    //                where pr.UserId == user.UserId
                    //                select pr).FirstOrDefault();

                    var prEty = _acoubntContext.PasswordResetModels.FirstOrDefault(x => x.UserId == userEty.UserId);
                    
                    _acoubntContext.PasswordResetModels.Remove(prEty);
                    _acoubntContext.Entry(prEty).State = System.Data.Entity.EntityState.Deleted;
                    bresult = true;

                    _acoubntContext.SaveChanges();
                }
            }

            return bresult;
        }


    }
    //public class AccountStub
    //{
    //    public static List<User> GetAllUsers()
    //    {
    //        return new List<User>()
    //        {
    //            new User() {UserId=1, Username="prasadja", FirstName="Praasd", LastName="Jadhav", Email="prasadja@cybage.com" },
    //            new User() {UserId=2, Username="abc", FirstName="ABC", LastName="DEF", Email="abc@cybage.com" },
    //            new User() {UserId=3, Username="def", FirstName="DEF", LastName="GHI", Email="def@cybage.com" },
    //            new User() {UserId=4, Username="pqr", FirstName="PQR", LastName="QRST", Email="pqr@cybage.com" },
    //            new User() {UserId=5, Username="xyz", FirstName="XYZ", LastName="WXYZ", Email="xyz@cybage.com" },
    //        };
    //    }
    //}
}
