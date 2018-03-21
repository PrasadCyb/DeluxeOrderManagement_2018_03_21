using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DeluxeOM.Services;
using DeluxeOM.Models.Account;
using DeluxeOM.Models;
using System.Collections.Generic;
//using DeluxeOM.WebUI.Models;


namespace DeluxeOM.WebUI.Controllers
{
    [DeluxeOMAuthorize(Roles = "SystemAdmin")]
    public class ManageController : BaseController
    {
        private IManageService _service = null;
        private INotificationService _notificationService = null;
        string tmpMsg = "Message";

        /// <summary>
        /// Create instance of ManageService, NotificationService
        /// </summary>
        public ManageController()
        {
            _service = new ManageService();
            _notificationService = new NotificationService();
        }
        // GET: User
        /// <summary>
        /// Disply all Users
        /// </summary>
        /// <returns>Users View</returns>
        public ActionResult Users()
        {
            var userList = _service.GetAllUsers();
            //User user = new User();
            //user.Name = Session["Name"].ToString();
            if (TempData[tmpMsg] != null)
            {
              
                ViewBag.Message = (string)TempData[tmpMsg];
            }
            return View(userList);
        }
        
        /// <summary>
        /// Not in Use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Users_new(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// Configuration of FTP
        /// </summary>
        /// <param name="returnUrl">returnUrl is not in use</param>
        /// <remark>This method only render static page for FTP configuration</remark>
        /// <returns>ConfigueFtp View</returns>
        [AllowAnonymous]
        public ActionResult ConfigureFtp(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// Display List of Notifications
        /// </summary>
        /// <param name="returnUrl">Not in use</param>
        /// <returns>Notification Mapping View</returns>
        [AllowAnonymous]
        public ActionResult NotificationMapping(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            NotificationMgt notificationMgt = new NotificationMgt();
            var notification=_notificationService.GetAllNotification();
            notificationMgt.Notifications = notification;
            return View(notificationMgt);
        }

        /// <summary>
        /// Update Notifications
        /// </summary>
        /// <param name="notifications">Contains a list of notifications which need to update</param>
        /// <returns></returns>
        public ActionResult UpdateNotification(List<Notification> notifications)
        {
            _notificationService.UpdateNotification(notifications);
            return RedirectToAction("NotificationMapping");
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Vid(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Territory(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult MonitorImport(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VidTerritory(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ViewJobStatus(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // GET: Customers
        /// <summary>
        /// Display List of Customers
        /// </summary>
        /// <param name="returnUrl">Not in use</param>
        /// <remark>This method display static list of customers</remark>>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Customers(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Display screen to add user in system
        /// </summary>
        /// <returns>User View</returns>
        public ActionResult NewUser()
        {
            var user = new DeluxeOM.Models.Account.User()
            {
                Active = true ,
                Roles = _service.GetRoles(),
                SelectedRoleId = 4
            };
            return View(user);
        }

        /// <summary>
        /// Add user in the System
        /// </summary>
        /// <param name="model">model contains user details</param>
        /// <returns>User View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(User model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _service.GetRoles();
                return View(model);
            }

            SaveResult result = _service.SaveUser(model);
            if (result.Success)
            {
                TempData[tmpMsg] = "Create new User successful";
                return RedirectToAction("Users");
            }
            else
            {
                model.Roles = _service.GetRoles();
                ViewBag.Message = result.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Search User for edit
        /// </summary>
        /// <param name="id">User ID for Edit</param>
        /// <returns>User View</returns>
        public ActionResult EditUser(int id)
        {

            var user = _service.GetUserById(id);
            if (user != null)
            {
                user.Roles = _service.GetRoles();
                ViewBag.SelectedRoles = new SelectList(user.Roles, "RoleId", "RoleName", user.Role);
                return View(user);
            }

            ViewBag.Message = "User not found.";
            return View();
        }

        /// <summary>
        /// Edit and save selected user
        /// </summary>
        /// <param name="model">model contains all details of the user which is selected for edit</param>
        /// <returns>User View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var saveStatus = _service.UpdateUser(model);
            if (saveStatus.Success)
            {
                TempData[tmpMsg] = "User updated successfully";
                return RedirectToAction("Users");
            }
            else
            {
                ViewBag.Message = "Unable to save user changes.";
                return View(model);
            }
        }

        [HttpPost]
        // GET: Users/Delete/5
        public JsonResult DeleteUser(int id)
        {
            var saveStatus = _service.DeleteUser(id);
            JsonResult result = new JsonResult();
            if (saveStatus.Success)
            {
                result.Data = "true";
                return result;
            }

            ViewBag.Message = "User not found.";
            result.Data = "false";

            return result;
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult AssignRoles(int userId)
        {
            User user = _service.GetUserById(userId);
            return View(user);
        }


        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRoles(User model)
        {
            SaveResult result = _service.AssignRoles(model.UserId, model.Roles);
            TempData[tmpMsg] = "Roles assigned successfully";
            return View("Users");
        }

        /// <summary>
        /// Display view for change password
        /// </summary>
        /// <returns>ChangePassword View</returns>
        public ActionResult ChangePassword()
        {
            var passReset = new ResetPasswordViewModel();
            passReset.Email = this.CurrentUser.Email;

            return View(passReset);
        }

        /// <summary>
        /// Update password of current user
        /// </summary>
        /// <param name="model">It Contains updated password</param>
        /// <returns>User View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PasswordReset pwdResetModel = new PasswordReset()
            {
                Email = model.Email,
                Password = model.Password
            };
            var result = _service.ChangePassword(pwdResetModel);
            if (result.Success)
            {
                //return RedirectToAction("Edit", new { id = User.userid });
                return RedirectToAction("Users");
            }

            ViewBag.Message = "Error resetting password.  Please try again. " + result.Message;
            return View();
        }

        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Validate email Id is exist or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonResult IsUserEmailAvailable(string email)
        {
            return Json(!_service.EmailExists(email), JsonRequestBehavior.AllowGet);
        }


    }
}