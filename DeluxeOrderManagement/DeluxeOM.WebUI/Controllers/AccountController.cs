using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DeluxeOM.WebUI.Models;
using DeluxeOM.Services;
using DeluxeOM.Models;
using DeluxeOM.Models.Account;
//using DeluxeOM.Services;

namespace DeluxeOM.WebUI.Controllers
{
    [DeluxeOMAuthorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAccountService _service = null;
        public AccountController()
        {
            _service = new AccountService();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Scapholding template
        /// </summary>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        /// <summary>
        /// Scapholding template
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        /// <summary>
        /// GET Login page
        /// </summary>
        /// <param name="returnUrl">URL that need to be loaded when logged in</param>
        /// <returns>Login view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            if (Request.Cookies["DeluxeOMUsername"] != null)
            {
                model.Email = Request.Cookies["DeluxeOMUsername"].Values["Username"];
                model.Password = Request.Cookies["DeluxeOMUsername"].Values["Password"];
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Junk Action method to test CSS and JQuery
        /// </summary>
        [AllowAnonymous]
        public ActionResult Login_new(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// POST login page
        /// </summary>
        /// <param name="model">Data along with UserName and Password</param>
        /// <param name="returnUrl">Page will be loaded after successful login</param>
        /// <returns>After successful logon returns landing page</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            User userModel = new DeluxeOM.Models.Account.User()
            {
                Email = model.Email,
                Password = model.Password,
            };
            if (userModel.Equals(null) && ModelState.IsValid)
            {
                return View(model);
            }

          
            var authResult = _service.Authenticate(userModel);

            User user = (User)authResult.DataObject;
            if (authResult.Status == AuthStatus.Success)
            {

                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.FirstName));
                user.Privs.ForEach((role) => identity.AddClaim(new Claim(ClaimTypes.Role, role.PrivName)));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                identity.AddClaim(new Claim(ClaimTypes.Sid, user.UserId.ToString()));
                if (!string.IsNullOrEmpty(user.PhoneNumber))
                {
                    identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                }
                //bool pippo = User.IsInRole("Admin");
                AuthenticationManager.SignIn(identity);
                //bool bippo = User.IsInRole("Admin");

                // if remember me clicked, save login name in cookie
                if (model.RememberMe)
                {
                    var userNameCookie = new HttpCookie("DeluxeOMUsername");
                    userNameCookie.Values["Username"] = model.Email;

                    userNameCookie.Expires = DateTime.Now.AddDays(14);
                    Response.Cookies.Add(userNameCookie);
                }
                else
                {
                    // remove cookie if is remember me cleared
                    if (Request.Cookies["DeluxeOMUsername"] != null)
                    {
                        var userNameCookie = new HttpCookie("DeluxeOMUsername");
                        userNameCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(userNameCookie);
                    }
                }

                // Login Successful
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Orders", "Order");

                }
            }
            else
            {
                // Login Failed
                if (authResult.Status == AuthStatus.PasswordExpiredAllowChange)
                {
                    ResetPasswordViewModel resetModel = new ResetPasswordViewModel()
                    {
                        Email = model.Email
                    };

                    //return RedirectToAction("ExpiredPassword", "Account", resetModel);
                    return RedirectToAction("ExpiredPassword","Account", new { email = resetModel.Email });
                }
                //ViewBag.Message = authResult.Message;
              
                    ModelState.AddModelError("", authResult.Message);
               
            }


            return View(model);

            #region Original
            //// This doesn't count login failures towards account lockout
            //// To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //} 
            #endregion
        }

        /// <summary>
        /// Scaphlding autogenerated code
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        /// <summary>
        /// Autogenerated code
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Autogenarted code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Autogenerated code
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        /// <summary>
        /// GET ForgoPassword page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// POST ForgotPassword data along with email address
        /// </summary>
        /// <param name="model">Contains email address</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PasswordReset pwdResetModel = new PasswordReset() { Email = model.Email };
            SaveResult result = _service.ForgotPassword(pwdResetModel);
            if (result.Success)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            ViewBag.Message = "Error processing password recovery request. " + result.Message;
            ViewBag.Email = model.Email;
            return View();
        }

        /// <summary>
        /// Display ForgotPassword success confirmation page
        /// </summary>
        /// <returns>ForgotPasswordConfirmation view</returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// GET ResetPassword page loaded when clicked on email link
        /// </summary>
        /// <param name="token">System generated token sent in link through email</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            // validate token
            var model = new ResetPasswordViewModel();
            User user = _service.VerifyPasswordReset(token);

            if (user != null)
            {
                var passwordModel = new ResetPasswordViewModel();
                passwordModel.Email = user.Email;
                passwordModel.Token = model.Token;

                return View(passwordModel);
            }

            ViewBag.Message = "Error verifying password recovery request. It is either not valid or has expired. ";
            return View();
        }

        /// <summary>
        /// POST ResetPassword page
        /// </summary>
        /// <param name="model">Containing old password and new password</param>
        /// <returns>resetPassword Confirmation</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PasswordReset rpwdModel = new PasswordReset()
            {
                Password = model.Password,
                Email = model.Email,
                Token = model.Token
            };
            SaveResult result = _service.ResetPassword(rpwdModel);
            if (result.Success)
            {
                // Login after user resets forgotten password 
                LoginViewModel loginModel = new LoginViewModel()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                return Login(loginModel, "");

            }
            else
            {
                ViewBag.Message = "Error resetting password.  Please try again. " + result.Message;
                return View();
            }
        }

        /// <summary>
        /// GET Reset Password Confirmation page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// GET PasswordExpiration page
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ExpiredPassword View</returns>
        [AllowAnonymous]
        public ActionResult ExpiredPassword(string email)
        {
            var passReset = new ResetPasswordViewModel();
            passReset.Email = email;

            return View(passReset);
        }

        /// <summary>
        /// POST Expire password page
        /// </summary>
        /// <param name="model">Contains updated password</param>
        /// <returns>On success returns Login View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExpiredPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var webApiResponse = await this.WebApiPost("api/password/expired", false, model);
            PasswordReset pwdResetModel = new PasswordReset() { Username = model.Email, Email = model.Email, Password = model.Password };
            SaveResult result = _service.ExpiredPassword(pwdResetModel);
            if (result.Success)
            {
                // Login after user changes password on Locked account
                // (New User, Admin Password Reset, Expired Password < 3 days)
                LoginViewModel loginModel = new LoginViewModel()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                return Login(loginModel, "");
            }

            ViewBag.Message = "Error resetting password.  Please try again. " + result.Message;
            return View();
        }



        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }


        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }


        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        /// <summary>
        /// Sign out from application
        /// </summary>
        /// <returns>Login View</returns>
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="result"></param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Scapholding autogenerated code
        /// </summary>
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}