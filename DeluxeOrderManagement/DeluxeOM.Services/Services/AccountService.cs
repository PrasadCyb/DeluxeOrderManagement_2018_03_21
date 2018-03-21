using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DeluxeOM.Models.Account;
using DeluxeOM.Services.Common;
using DeluxeOM.Models;
using DeluxeOM.Models.Common;
using DeluxeOM.Repository;
using DeluxeOM.Utils.Config;


namespace DeluxeOM.Services
{
    /// <summary>
    /// Methods for password management and user authentication
    /// </summary>
    public class AccountService : ServiceBase, IAccountService
    {
        IAccountRepository _accountRepository = null;
        /// <summary>
        /// Create instance of AccountRepository
        /// </summary>
        public AccountService(/*IAccountRepository accountRepository*/)
        {
            //can inject repository for test cases
            _accountRepository = new AccountRepository();
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="user">Details of user including password and user name</param>
        /// <returns>Status of authentication</returns>
        public AuthResult Authenticate(User user)
        {
            //move to config
            int MaxLoginAttempts = App.Config.MaxLoginAttempts;
            int PasswordExpiryDays = App.Config.PasswordExpiryDays;

            //get hash from db
            //1. Lock user
            //2. Max attemps 
            //3. password expired
            //GetAuthMessage(AuthResult result)
            var ety = _accountRepository.GetUserByUserName(user.Email);
            AuthResult authResult = new AuthResult();
            if (ety != null)
            {
                User dbuser = this.Mapper.CreateUserModel(ety);
                AuthStatus authStatus = AuthStatus.None;
                string authMsg = string.Empty;

                // If User Exists
                if (dbuser != null && dbuser.UserId > 0)
                {
                    if (dbuser.LoginAttempts >= MaxLoginAttempts)
                    {
                        _accountRepository.IncrementLoginAttempts(dbuser.Username);
                        // If LoginAttemps > <MaxLoginAttempts>
                        // Log LoginFailure: Account Locked. AuthAudit Entry "Max Login Attempts Exceeded"
                        authStatus = AuthStatus.MaxLoginAttemptsExceeded;
                        authMsg = "Max Login Attempts Exceeded";
                    }
                    else if (!SimpleHash.VerifyHash(user.Password, "SHA1", dbuser.Password))
                    {
                        _accountRepository.IncrementLoginAttempts(dbuser.Username);
                        // Log LoginFailure: Invalid Password. AuthAudit Entry "Invalid UserID/Password combination"
                        authStatus = AuthStatus.InvalidCredentials;
                        authMsg = "Invalid credentials.";
                    }
                    else if (PasswordExpiryDays != 0 && dbuser.PasswordDatetime.Value.AddDays(PasswordExpiryDays) < DateTime.UtcNow)
                    {
                        // If PasswordExpiryDays = 0 (No Password Expiration)
                        // If PasswordDate + <ExipryDays> < Now 
                        // Log LoginFailure AuthAudit Entry "Password Expired" 

                        // If PasswordDate + <ExpiryDays + 3> < Now
                        // Allow change of password
                        if (dbuser.PasswordDatetime.Value.AddDays(PasswordExpiryDays + 3) < DateTime.UtcNow)
                        {
                            authStatus = AuthStatus.PasswordExpired;
                        }
                        else
                        {
                            authStatus = AuthStatus.PasswordExpiredAllowChange;
                        }

                        authMsg = "Password Expired";
                    }
                    else
                    {
                        // LoginSuccess AuthAudit Entry
                        authStatus = AuthStatus.Success;
                        authMsg = "Login succeeded";
                        _accountRepository.SetLastLoginInfo(dbuser.UserId);
                    }
                }
                else
                {
                    // Log LoginFailure: User Not Found. AuthAudit Entry "Invalid UserID/Password combination"
                    authStatus = AuthStatus.InvalidCredentials;
                    authMsg = "Invalid credentials";
                    _accountRepository.IncrementLoginAttempts(user.Username); // Needed ??
                }

                authResult = new AuthResult()
                {
                    Status = authStatus,
                    Message = GetAuthMessage(authStatus),
                    DataObject = (Object)dbuser
                };

            }
            else
            {
                authResult = new AuthResult()
                {
                    Status = AuthStatus.InvalidCredentials,
                    Message = "Invalid credentials"
                };
            }

            // Return User object
            return authResult;
        }


        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string[] GetRoles(string userName)
        {
            //return new string[] { "SystemAdmin", "SecurityAdmin" };
            return new string[] { };
        }

        /// <summary>
        /// Check password reset token is expired or not
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public User VerifyPasswordReset(string token)
        {
            User user = null;
            var userEty = _accountRepository.VerifyPasswordReset(token);
            if (userEty != null)
            {
                user = this.Mapper.CreateUserModel(userEty);
            }

            return user;
        }

        /// <summary>
        /// Create new password
        /// </summary>
        /// <param name="passwordReset">Contains password details</param>
        /// <returns>True if password reset successfully else False</returns>
        public SaveResult ResetPassword(PasswordReset passwordReset)
        {
            passwordReset.Password = SimpleHash.ComputeHash(passwordReset.Password, "SHA1", null);
            if (_accountRepository.ResetPassword(passwordReset))
                return SaveResult.SuccessResult;
            else
                return SaveResult.FailureResult("Password has been expired");
        }

        /// <summary>
        /// Insert Guid and email with current time into PasswordReset Table
        /// </summary>
        /// <param name="passwordresetModel"></param>
        /// <returns>Ture on successful insert</returns>
        public SaveResult ForgotPassword(PasswordReset passwordresetModel)
        {
            //in minutes
            int ForgotPasswordInterval = App.Config.ForgotPasswordInterval;

            passwordresetModel.ExpireDatetime = DateTime.UtcNow.AddMinutes(ForgotPasswordInterval);
            passwordresetModel.Token = Guid.NewGuid().ToString("N");

            var ety = this.Mapper.CreatePasswordResetEntity(passwordresetModel);
            if (_accountRepository.ForgotPassword(ety))
            {
                var model = this.Mapper.CreatePasswordResetModel(ety);
                // CreateForgotPasswordNotification: Insert email into Notification Table
                if (CreateForgotPasswordNotification(model))
                {
                    return SaveResult.SuccessResult;
                }
            }
            return SaveResult.FailureResult("");
        }

        /// <summary>
        /// Create new password
        /// </summary>
        /// <param name="model">Contains password details</param>
        /// <returns>Success Result</returns>
        public SaveResult ExpiredPassword(PasswordReset model)
        {
            model.Password = SimpleHash.ComputeHash(model.Password, "SHA1", null);
            _accountRepository.ExpiredPassword(model);
            return SaveResult.SuccessResult;
        }

        /// <summary>
        /// Create anthenication message
        /// </summary>
        /// <param name="status">Status of authentication</param>
        /// <returns>Authentication message</returns>
        private string GetAuthMessage(AuthStatus status)
        {
            string message = string.Empty;

            switch (status)
            {
                case AuthStatus.InvalidCredentials:
                    message = "Invalid User ID / Password combination, please try again.";
                    break;
                case AuthStatus.MaxLoginAttemptsExceeded:
                    message = "Account is locked and may not logon, contact your Security Administrator to unlock your account.";
                    break;
                case AuthStatus.PasswordExpired:
                    message = "Password has expired, contact your Security Administrator to reset your password.";
                    break;
                case AuthStatus.PasswordExpiredAllowChange:
                    message = "Password has expired, please change your password.";
                    break;
                case AuthStatus.DatabaseError:
                default:
                    message = "There was a problem performing the Login request, please try again.";
                    break;
            }
            return message;
        }

        /// <summary>
        /// Send Email notification for forgot password
        /// </summary>
        /// <param name="model"></param>
        /// <returns>True</returns>
        private bool CreateForgotPasswordNotification(PasswordReset model)
        {
            bool retVal = true;

            string uiBaseUrl = App.Config.UIBaseUrl; 
            string action = App.Config.RecoverPasswordAction;
            int expireMins = App.Config.ForgotPasswordInterval;
            string txtLink = string.Format(action, model.Token);

            string adminEmail = App.Config.AdminEmailAddress;

            //string emailBody = string.Format("Hi, <br> You recently requested to reset your password to your DeluxeOrderManagement account. Click the button below to reset it. <br> <a href={0}. <br>This link is only valid for {1} next minutes>", txtLink, expireMins.ToString());
            string emailBody = string.Format("Hi, <html><body><br> You recently requested to reset your password to your DeluxeOrderManagement account. Click the button below to reset it. <br> <a href={0} <br>This link is only valid for {1} next minutes</body></html>", uiBaseUrl+txtLink, expireMins.ToString());

            INotificationService nfnService = new NotificationService();
            dlxMailMessage message = new dlxMailMessage()
            {
                From = adminEmail,
                SuccessBody = emailBody,
                SuccessSubject = "Forgot Password",
                To = model.Email
            };
            
            nfnService.SendEmail(message);

            /*
            htp://om.domain.com/resetpassword/tokenguid
            bool retVal = false ;

            string uiBaseUrl = Config.UIBaseUrl;    // https://om.deluxe.com
            string controller = Config.RecoverPasswordController";
            string action = Config.RecoverPasswordAction";
            int expireMins = Config.ForgotPasswordInterval";

            string adminEmail = Config.AdminEmailAddress;
            string txtLink = string.Format(Resources.Account.ResetPasswordLinkText, uiBaseUrl, controller, action, HttpUtility.UrlEncode(model.Token));
            string htmlLink = string.Format(Resources.Account.ResetPasswordLinkHtml, uiBaseUrl, controller, action, HttpUtility.UrlEncode(model.Token));
            string txtBody = string.Format(Resources.Account.ResetPasswordBodyText, model.FirstName, model.LastName, adminEmail, txtLink, expireMins);
            string htmlBody = string.Format(Resources.Account.ResetPasswordBodyHtml, model.FirstName, model.LastName, adminEmail, htmlLink, expireMins);

            NotifyItem mail = new NotifyItem();
            mail.NotifyType = Core.NotifyType.SmtpMail;
            mail.ContentType = Core.NotifyContentType.PasswordChange;
            mail.NotifyDatetime = DateTime.Now;
            mail.Address = model.Email;
            mail.Subject = Resources.Account.ForgotPasswordSubject;
            mail.BodyText = txtBody;
            mail.BodyHtml = htmlBody;
            mail.IsBodyHtml = true;

            if (_notifyDB.Add(mail) == 1)
                retVal = true;
            */
            return retVal;
        }

    }
}
