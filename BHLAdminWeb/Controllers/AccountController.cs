using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using MOBOT.BHL.AdminWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new BHLUserManager(new UserStore<ApplicationUser, CustomRole, int, 
                CustomUserLogin, CustomUserRole, CustomUserClaim>(new ApplicationDbContext())))
        {
        }

        //public AccountController(UserManager<ApplicationUser> userManager)
        public AccountController(BHLUserManager userManager)
        {
            UserManager = userManager;

            // Use this to enforce stronger password rules
            UserManager.PasswordValidator = new PasswordValidator
            {
                //RequireDigit = true,
                RequiredLength = 10,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequireUppercase = true
            };

            // Set up a user token provider for use when replacing forgotten passwords.
            // Token will expire 24 hours after being issued.
            var provider = new DpapiDataProtectionProvider("BhlAdmin");
            UserManager.UserTokenProvider =
                new DataProtectorTokenProvider<ApplicationUser, int>(
                    provider.Create("PasswordChange"))
                    {
                        TokenLifespan = TimeSpan.FromHours(24)
                    };


            // Set up an email service
            UserManager.EmailService = new MVCServices.EmailService();
        }

        public BHLUserManager UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (!user.Disabled)
                    {

                        await SignInAsync(user, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please contact an administrator for access to this site.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            // If a user is already authenticated, then we know this is a BHL admin adding a new user account
            bool authenticated = Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (authenticated)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        await SignInAsync(user, isPersistent: false);

                        // Send the user an email letting them know that an administrator will assign
                        // roles to the new account.  Copy the BHL user administrator on the message.
                        int userId = user.Id;
                        string emailBody = string.Format(
                            "Your new user account has been registered successfully.\n\r" +
                            "Username: {0}\r" +
                            "First Name: {1}\r" +
                            "Last Name: {2}\r" +
                            "Email Address: {3}\n\r" +
                            "A BHL administrator will now assign the appropriate permissions to your account.  When that is complete, you will have access to expanded BHL functionality.\n\r" +
                            "The administrator will be notify you by email when the permissions have been assigned. Thank you for your patience.",
                            user.UserName, user.FirstName, user.LastName, user.Email);
                        List<string> bccList = new List<string>();
                        bccList.Add(ConfigurationManager.AppSettings["BHLUserAdminEmailAddress"]);
                        await UserManager.SendEmailAsync(userId, new List<string>(), bccList, "Your new BHL user account", emailBody);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId<int>(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId<int>(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                user.Email = model.Email;

                try
                {
                    var result = await UserManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        result = await UserManager.AddLoginAsync(user.Id, info.Login);
                        if (result.Succeeded)
                        {
                            await SignInAsync(user, isPersistent: false);

                            // Send the user an email letting them know that an administrator will assign
                            // roles to the new account.  Copy the BHL user administrator on the message.
                            int userId = user.Id;
                            string emailBody = string.Format(
                                "Your new user account has been registered successfully.\n\r" +
                                "Username: {0}\r" +
                                "Email Address: {1}\n\r" +
                                "A BHL administrator will now assign the appropriate permissions to your account.  When that is complete, you will have access to expanded BHL functionality.\n\r" +
                                "The administrator will be notify you by email when the permissions have been assigned. Thank you for your patience.",
                                user.UserName, user.Email);
                            List<string> bccList = new List<string>();
                            bccList.Add(ConfigurationManager.AppSettings["BHLUserAdminEmailAddress"]);
                            await UserManager.SendEmailAsync(userId, new List<string>(), bccList, "Your new BHL user account", emailBody);

                            return RedirectToLocal(returnUrl);
                        }
                    }
                    AddErrors(result);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {

                }

            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId<int>());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Index()
        {
            // Only users in the SysAdmin role are allowed to delete user accounts
            bool canDelete = Request.GetOwinContext().Authentication.User.IsInRole(MOBOT.BHL.AdminWeb.Helper.SecurityRole.BHLAdminSysAdmin.ToString());

            var Db = new ApplicationDbContext();
            var users = Db.Users.Where(r => r.Id != 1).OrderBy(r => r.UserName);
            var model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                var u = new EditUserViewModel(user);
                u.AllowDelete = canDelete;
                u.HasRoles = (user.Roles.ToArray().Count() > 0);

                model.Add(u);
            }
            return View(model);
        }


        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Edit(string id, ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Disabled = model.Disabled;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await Db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [Authorize(Roles = "BHL.Admin.SysAdmin")]
        public ActionResult Delete(string id = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BHL.Admin.SysAdmin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            Db.Users.Remove(user);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult UserRoles(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new SelectUserRolesViewModel(user);
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoles(SelectUserRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var idManager = new IdentityManager();
                var Db = new ApplicationDbContext();
                var user = Db.Users.First(u => u.UserName == model.UserName);
                idManager.ClearUserRoles(user.Id);
                foreach (var role in model.Roles)
                {
                    if (role.Selected)
                    {
                        idManager.AddUserToRole(user.Id, role.RoleName);
                    }
                }
                return RedirectToAction("index");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Forgot()
        {
            ViewBag.EmailSent = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Forgot(ForgotViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verify the email address
                var user = await UserManager.FindByEmailAsync(model.Email);

                // Make sure we have a valid user; if not, just act like we do (to prevent malicious use)
                if (user != null)
                {
                    int userId = user.Id;

                    // Generate a password reset token
                    string resetToken = await UserManager.GeneratePasswordResetTokenAsync(userId);

                    var callbackUrl = Url.Action("ResetPassword", "Account", new { id = user.Id, token = resetToken}, protocol: Request.Url.Scheme);
                    string emailBody = "Reset your password by navigating to " + callbackUrl + ".\n\rIf you did not initiate this request, then please ignore this message.";
 
                    // Send the email
                    await UserManager.SendEmailAsync(userId, null, null, "Reset your BHL password", emailBody);
                }
                ViewBag.EmailSent = true;
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string id, string token)
        {
            ViewBag.PasswordReset = false;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.PasswordReset = true;

                // Verify the id
                var user = await UserManager.FindByIdAsync(Convert.ToInt32(model.Id));

                // Make sure we have a valid user; if not, just act like we do (to prevent malicious use)
                if (user != null)
                {
                    // Reset the password
                    var result = await UserManager.ResetPasswordAsync(user.Id, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        ViewBag.PasswordReset = false;
                    }
                }
            }
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
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
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
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