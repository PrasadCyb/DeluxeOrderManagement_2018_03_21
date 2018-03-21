using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models;
using DeluxeOM.Models.Account;

namespace DeluxeOM.Repository
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Get all user from system
        /// </summary>
        /// <returns>List of users</returns>
        public List<UserModel> GetAll()
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var userety = _context.UserModels
                    .Include("UserPasswords")
                    .Include("UserRoles")
                    .Include("UserRoles.Role.RolePrivs.Privilege")
                    //.Where(x => x.Active)
                    .ToList();
                return userety;
            }
        }

        /// <summary>
        /// Search user by Id
        /// </summary>
        /// <param name="id">userId</param>
        /// <returns>User</returns>
        public UserModel GetById(int id)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var userety = _context.UserModels
                    .Include("UserPasswords")
                    .Include("UserRoles")
                    .Include("UserRoles.Role.RolePrivs.Privilege")
                    .FirstOrDefault(x => x.UserId == id);
                return userety;
            }

        }


        /// <summary>
        /// Search user by email
        /// </summary>
        /// <param name="email">user email id</param>
        /// <returns>User</returns>
        public UserModel GetByEmail(string email)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var userety = _context.UserModels
                    .Include("UserPasswords")
                    .Include("UserRoles")
                    .Include("UserRoles.Role.RolePrivs.Privilege")
                    .FirstOrDefault(x => x.Email == email && x.Active);
                return userety;
            }

        }


        /// <summary>
        /// Add new user in system
        /// </summary>
        /// <param name="user"></param>
        public void Save(UserModel user)
        {   
            using (var _context = new DeluxeOrderManagementEntities())
            {
                _context.UserModels.Add(user);
                _context.Entry(user).State = System.Data.Entity.EntityState.Added;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Update user in system
        /// </summary>
        /// <param name="user">user details</param>
        public void Update(UserModel user)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var ety = _context.UserModels.FirstOrDefault(x => x.UserId == user.UserId);
                if (ety != null)
                {
                    ety.UserName = user.Email;
                    ety.FirstName = user.FirstName;
                    ety.LastName = user.LastName;
                    ety.Email = user.Email;
                    ety.PhoneNo = user.PhoneNo;
                    ety.Title = user.Title;
                    ety.Active = user.Active;
                    ety.ModifiedDate = user.ModifiedDate;

                    var userroles = ety.UserRoles.Where(x => x.UserId == user.UserId);
                    if (userroles != null && userroles.Any())
                    {
                        foreach (var userrole in userroles.ToList())
                        {
                            var ur = ety.UserRoles.FirstOrDefault(x => x.UserId == userrole.UserId && x.RoleId == userrole.RoleId);
                            ety.UserRoles.Remove(ur);
                            _context.Entry(ur).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }

                    foreach (var selectedrole in user.UserRoles)
                    {
                        var insertedRole = new Repository.UserRoleModel()
                        {
                            UserId = user.UserId,
                            RoleId = selectedrole.RoleId
                        };
                        ety.UserRoles.Add(insertedRole);
                        _context.Entry(insertedRole).State = System.Data.Entity.EntityState.Added;
                    }

                    _context.Entry(ety).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Delete user from system
        /// </summary>
        /// <param name="id">userId</param>
        public void Delete(int id)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var ety = _context.UserModels.FirstOrDefault(x => x.UserId == id);
                if (ety != null)
                {
                    //var upeties = _context.UserPasswordModels.Where(x => x.UserId == id);
                    //foreach (var up in upeties)
                    //{
                    //    ety.UserPasswords.Remove(up);
                    //    _context.Entry(up).State = System.Data.Entity.EntityState.Deleted;
                    //}


                    //_context.UserModels.Remove(ety);
                    ety.Active = false;
                    _context.Entry(ety).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Change password of current user
        /// </summary>
        /// <param name="upEntity">Password details</param>
        public void ChangePassword(UserPasswordModel upEntity)
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var ety = _context.UserModels.FirstOrDefault(e => e.UserId == upEntity.UserId);

                if (ety != null)
                {
                    _context.UserPasswordModels.Add(upEntity);
                    _context.Entry(upEntity).State = System.Data.Entity.EntityState.Added;

                    ety.LoginAttempts = 0;
                    _context.Entry(ety).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Get all roles from system
        /// </summary>
        /// <returns>List of role</returns>
        public List<RoleModel> GetRoles()
        {
            using (var _context = new DeluxeOrderManagementEntities())
            {
                var roleety = _context.RoleModels
                    .Where(x => x.RoleName.ToLower() != "root" && x.Active)
                    .ToList();
                return roleety;
            }
        }


        /// <summary>
        /// Role Name of user
        /// </summary>
        /// <param name="userID">userID</param>
        /// <returns>Role Name</returns>
        public string GetUserRoleName(int userID)
        {
            var _context = new DeluxeOrderManagementEntities();

            var userRoleName = (from role in _context.RoleModels
                                join userRole in _context.UserRoleModels on role.RoleId equals userRole.RoleId
                                where userRole.UserId == userID
                                select role.RoleName.ToString()
                                ).FirstOrDefault().ToString();
            return userRoleName;
        }
    }

    //public class UserStub
    //{
    //    public static List<User> GetAll()
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
