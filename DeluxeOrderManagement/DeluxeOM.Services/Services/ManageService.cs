using System;
using System.Collections.Generic;
using System.Linq;
using DeluxeOM.Models;
using DeluxeOM.Models.Account;
using DeluxeOM.Repository;

using DeluxeOM.Services.Common;


namespace DeluxeOM.Services
{
    public class ManageService : ServiceBase,IManageService
    {
        private IUserRepository _userRepository = null;
        
        public ManageService(/*IUserRepository userRepository*/)
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Populates all users
        /// </summary>
        /// <returns>List of users</returns>
        public List<User> GetAllUsers()
        {
            var eties = _userRepository.GetAll();
            List<User> users = new List<User>();
            if (eties != null && eties.Any())
            {
                foreach (var ety in eties )
                    users.Add(this.Mapper.CreateUserModel(ety));
            }
            return users;
        }

        /// <summary>
        /// Populate user depending on perticular id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        public User GetUserById(int userId)
        {
            var ety = _userRepository.GetById(userId);
            return this.Mapper.CreateUserModel(ety);
        }

        /// <summary>
        /// Add user in the System
        /// </summary>
        /// <param name="user">model contains user details</param>
        /// <returns>SaveResult</returns>
        public SaveResult SaveUser(User user)
        {
            string message = string.Empty;
            bool success = false;
            if (EmailExists(user.Email))
            {
                message = "User email already exists. Please select another email.";
            }
            else
            {
                //System.Web.Security.Membership.GeneratePassword(12, 3);
                var passwordHash = SimpleHash.ComputeHash(user.Password, "SHA1", null);
                user.Password = passwordHash;
                user.PasswordDatetime = DateTime.UtcNow;
                user.CreateDatetime = DateTime.UtcNow;
                user.ModifyDatetime = DateTime.UtcNow;
                var userEty = this.Mapper.CreateUserEntity(user);
                _userRepository.Save(userEty);
                success = true;
            }

            return new SaveResult() { Success = success, Message = message };
        }

        /// <summary>
        /// Removing user from system
        /// </summary>
        /// <param name="userId">userId for removing user from database</param>
        /// <returns>SaveResult</returns>
        public SaveResult DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
            return SaveResult.SuccessResult;
        }

        /// <summary>
        /// Update user details in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns>SaveResult</returns>
        public SaveResult UpdateUser(User user)
        {
            string message = string.Empty;
            bool success = false;
            if (EmailExists(user.Email, user.UserId))
            {
                message = "User email already exists. Please select another email.";
            }
            else
            {
                var userEntity = _userRepository.GetById(user.UserId);
                if (userEntity != null)
                {
                    userEntity.UserName = user.Username;
                    userEntity.FirstName = user.FirstName;
                    userEntity.LastName = user.LastName;
                    userEntity.Email = user.Email;
                    userEntity.PhoneNo = user.PhoneNumber;
                    userEntity.Title = user.Title;
                    userEntity.Active = user.Active;
                    userEntity.ModifiedDate = DateTime.UtcNow;
                    var userroles = userEntity.UserRoles.Where(x => x.UserId == user.UserId);
                    if (userroles != null && userroles.Any())
                    {
                        foreach (var userrole in userroles.ToList())
                        {
                            var ur = userEntity.UserRoles.FirstOrDefault(x => x.UserId == userrole.UserId && x.RoleId == userrole.RoleId);
                            userEntity.UserRoles.Remove(ur);
                        }
                    }

                    //foreach (var selectedroleid in user.SelectedRoles)
                    //{
                    userEntity.UserRoles.Add(new Repository.UserRoleModel()
                    {
                        UserId = user.UserId,
                        RoleId = user.SelectedRoleId
                    });
                    //}

                    _userRepository.Update(userEntity);
                    success = true;
                }
                else
                {
                    message = "User does not exisst";
                }
                    
                
            }
            
            return new SaveResult() { Success = success, Message = message };
        }

        /// <summary>
        /// Update password of current user
        /// </summary>
        /// <param name="model">It Contains updated password</param>
        /// <returns>SaveResult</returns>
        public SaveResult ChangePassword(PasswordReset model)
        {
            bool success = false;
            string message = "";

            var userety = _userRepository.GetByEmail(model.Email);
            
            if (userety != null)
            {
                var passwordHash = SimpleHash.ComputeHash(model.Password, "SHA1", null);
                model.Password = passwordHash;
                model.UserId = userety.UserId;

                var upety = this.Mapper.CreateUserPasswordEntity(model);
                upety.PasswordDate = DateTime.UtcNow;
                upety.CreatedDate = DateTime.UtcNow;
                _userRepository.ChangePassword(upety);
                success = true;
            }
            else
            {
                success = false;
                message = "User does not exisst.";
            }
            
            return success ? SaveResult.SuccessResult
                : SaveResult.FailureResult(message);
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public SaveResult AssignRoles(int userId, List<RoleMembership> roles)
        {
            //yet ti implement
            return SaveResult.SuccessResult;
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SaveResult RemoveRole(int userId, int roleId)
        {
            //yet ti implement
            return SaveResult.SuccessResult;
        }

        /// <summary>
        /// Validate email Id is exist or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns>True if email exist else false </returns>
        public bool EmailExists(string email, int? userId = null)
        {
            var users = _userRepository.GetAll();
            bool emailWExists = false;
            if (users != null && users.Any())
            {
                if (userId.HasValue)
                    emailWExists = users.Any(x => x.Email == email && x.UserId != userId);
                else
                    emailWExists = users.Any(x => x.Email == email);

            }
            return emailWExists;
        }

        /// <summary>
        /// Populates all the roles
        /// </summary>
        /// <returns>Details of role</returns>
        public List<RoleMembership> GetRoles()
        {
            List<RoleMembership> roles = new List<RoleMembership>();
            foreach (var roleety in _userRepository.GetRoles())
            {
                roles.Add(this.Mapper.CreateRoleModel(roleety));
            }
            return roles;
        }

        
    }
}
