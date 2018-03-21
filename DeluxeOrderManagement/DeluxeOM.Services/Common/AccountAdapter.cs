using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeluxeOM.Models.Account;
using DeluxeOM.Services.Common;
using DeluxeOM.Models;
using DeluxeOM.Repository;
using DeluxeOM.Models.BulkUploader;

namespace DeluxeOM.Services.Common
{
    public class AccountAdapter
    {
        public UserModel CreateUserEntity(User userModel)
        {
            UserRepository userRepo = new UserRepository();

            UserModel ety = new UserModel()
            {
                UserId = userModel.UserId,
                UserName = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNo = userModel.PhoneNumber,
                //Role= userRepo.GetUserRoleName(userModel.UserId),
                Active = userModel.Active,
                LoginAttempts = userModel.LoginAttempts,
                LastLogin = userModel.LastLoginDatetime,
                CreatedDate = userModel.CreateDatetime,
                ModifiedDate = userModel.ModifyDatetime,
                
            };
            ety.UserPasswords.Add(new UserPasswordModel()
            {
                UserId = userModel.UserId,
                Password = userModel.Password,
                PasswordDate = userModel.PasswordDatetime.Value,
                CreatedDate = userModel.CreateDatetime
            });
            //if (userModel.SelectedRoles.Any())
            //{
            //    foreach (var roleid in userModel.SelectedRoles)
            //    {
            ety.UserRoles.Add(new UserRoleModel()
            {
                UserId = userModel.UserId,
                RoleId = userModel.SelectedRoleId
            });
            //    }
            //}


            return ety;
        }

        public User CreateUserModel(UserModel entity)
        {
            UserRepository userRepo = new UserRepository();

            User model = new User()
            {
                UserId = entity.UserId,
                Username = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.UserPasswords.LastOrDefault().Password ,
                PasswordDatetime = entity.UserPasswords.LastOrDefault().PasswordDate ,
                PhoneNumber = entity.PhoneNo,
                Role = userRepo.GetUserRoleName(entity.UserId),
                Active = entity.Active,
                LoginAttempts = entity.LoginAttempts,
                LastLoginDatetime = entity.LastLogin,
                CreateDatetime = entity.CreatedDate,
                ModifyDatetime= entity.ModifiedDate
            };
            if (entity.UserRoles != null)
            {
                foreach (var userrole in entity.UserRoles.Distinct())
                {
                    if (userrole.Role != null)
                    {
                        //model.SelectedRoles.Add(userrole.Role.RoleId);
                        model.SelectedRoleId = userrole.Role.RoleId;
                    }
                    
                    if (userrole.Role != null && userrole.Role.RolePrivs != null && userrole.Role.RolePrivs.Any())
                    {
                        foreach (var priv in userrole.Role.RolePrivs.Distinct())
                        {
                            model.Privs.Add(new Privilleges()
                            {
                                PrivId = priv.Privilege.PrivId,
                                PrivName = priv.Privilege.PrivName,
                                Description = priv.Privilege.Description
                            });
                        }
                    }
                }
            }
            

            return model;
        }


        public UserPasswordModel CreateUserPasswordEntity(PasswordReset model)
        {
            UserPasswordModel ety = new UserPasswordModel()
            {
                UserId = model.UserId,
                Password = model.Password,
            };
            return ety;
        }

        public PasswordResetModel CreatePasswordResetEntity(PasswordReset model)
        {
            PasswordResetModel ety = new PasswordResetModel()
            {
                UserId = model.UserId ,
                Email = model.Email ,
                Token = model.Token ,
                ExpireDateTime = model.ExpireDatetime 
            };
            return ety;
        }

        public PasswordReset CreatePasswordResetModel(PasswordResetModel entity)
        {
            PasswordReset model = new PasswordReset()
            {
                UserId = entity.UserId,
                Email = entity.Email,
                Token = entity.Token,
                ExpireDatetime = entity.ExpireDateTime
            };
            return model;
        }

        public RoleMembership CreateRoleModel(RoleModel entity)
        {
            RoleMembership model = new RoleMembership()
            {
                RoleId = entity.RoleId,
                RoleName = entity.RoleName,
                Description  = entity.Description
            };
            return model;
        }

        public RoleModel CreateRoleEntity(RoleMembership model)
        {
            RoleModel ety = new RoleModel()
            {
                RoleId= model.RoleId,
                RoleName = model.RoleName,
                Description = model.Description
            };
            return ety;
        }

        public Job CreateJobModel(JOB entity)
        {
            Job model = new Job()
            {
                Id = entity.Id ,
                Name = entity.JobType,
                Status = entity.Status,
                FileName  = entity.FileName,
                JobType = entity.JobType,
                Description = entity.Description + " " + (!string.IsNullOrEmpty(entity.FileName) ? "(" + entity.FileName + ")" : string.Empty),
                TriggeredBy = entity.TriggeredBy
            };

            return model;
        }

        public JobItem CreateJobItemModel(Jobs_Items entity)
        {
            JobItem model = new JobItem()
            {
                Id = entity.Id,
                JobId = entity.JobId,
                TaskName = entity.TaskName,
                Status = entity.Status,
                Description = entity.Description ,
                //StartDate = entity.StartDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(entity.StartDate.Value, TimeZoneInfo.Local) : entity.StartDate,
                StartDate = entity.StartDate,
                //EndDate = entity.EndDate.HasValue ? TimeZoneInfo.ConvertTimeToUtc(entity.EndDate.Value, TimeZoneInfo.Local) : entity.EndDate
                EndDate = entity.EndDate
            };
            
            return model;
        }
        
    }
}
