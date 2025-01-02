using CsvHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using MOBOT.BHL.AdminWeb.Models;
using MOBOT.BHL.Server;
using MOBOT.BHL.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MOBOT.BHL.AdminWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private int _roleNone = 0;
        private int _roleAny = -1;

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
                // Make sure the user account exists
                var user = await UserManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    // Validate the password
                    var validatedUser = await UserManager.FindAsync(model.UserName, model.Password);

                    if (user.Disabled)
                    {
                        // Disabled user account
                        ModelState.AddModelError("", "Please contact an administrator for access to this site.");
                    }
                    else if (await UserManager.IsLockedOutAsync(user.Id))
                    {
                        // User locked out
                        ModelState.AddModelError("", "Your account has been locked out due to multiple failed login attempts.");
                    }
                    else if (await UserManager.GetLockoutEnabledAsync(user.Id) && validatedUser == null)
                    {
                        // User is subject to lockouts and the credentials are invalid.  Record the failure.
                        await UserManager.AccessFailedAsync(user.Id);

                        // Check lockout status
                        if (await UserManager.IsLockedOutAsync(user.Id))
                        {
                            ModelState.AddModelError("", "Your account has been locked out due to multiple failed login attempts.");
                        }
                        else
                        {
                            int accessFailedCount = await UserManager.GetAccessFailedCountAsync(user.Id);
                            int attemptsLeft = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"].ToString()) - accessFailedCount;
                            ModelState.AddModelError("", "Invalid username or password.");
                        }
                    }
                    else if (validatedUser == null)
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                    else
                    {
                        await SignInAsync(user, model.RememberMe);
                        // Clear the access failed count
                        await UserManager.ResetAccessFailedCountAsync(user.Id);
                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    // Username does not exist
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

            ViewBag.Institutions = GetInstitutions();
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
                user.HomeInstitutionCode = model.HomeInstitutionCode;
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
                        string emailBody = System.IO.File.ReadAllText(Server.MapPath(@"\UserRegistrationEmail.txt"));
                        string helpLink = ConfigurationManager.AppSettings["WikiPageHelpDocs"];
                        emailBody = emailBody.Replace("<<<USERNAME>>>", user.UserName);
                        emailBody = emailBody.Replace("<<<FIRSTNAME>>>", user.FirstName);
                        emailBody = emailBody.Replace("<<<LASTNAME>>>", user.LastName);
                        emailBody = emailBody.Replace("<<<EMAILADDRESS>>>", user.Email);
                        emailBody = emailBody.Replace("<<<CONTENTPROVIDER>>>", model.HomeInstitutionName);
                        emailBody = emailBody.Replace("<<<HELPLINK>>>", helpLink);

                        List<string> bccList = new List<string>();
                        bccList.Add(ConfigurationManager.AppSettings["BHLUserAdminEmailAddress"]);
                        await UserManager.SendEmailAsync(userId, new List<string>(), bccList, "Your new BHL user account", emailBody);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    AddErrors(result);
                    ViewBag.Institutions = GetInstitutions();
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
                            string emailBody = System.IO.File.ReadAllText(Server.MapPath(@"\ExternalUserRegistrationEmail.txt"));
                            string helpLink = ConfigurationManager.AppSettings["WikiPageHelpDocs"];
                            emailBody = emailBody.Replace("<<<USERNAME>>>", user.UserName);
                            emailBody = emailBody.Replace("<<<EMAILADDRESS>>>", user.Email);
                            emailBody = emailBody.Replace("<<<HELPLINK>>>", helpLink);

                            List<string> bccList = new List<string>();
                            bccList.Add(ConfigurationManager.AppSettings["BHLUserAdminEmailAddress"]);
                            await UserManager.SendEmailAsync(userId, new List<string>(), bccList, "Your new BHL user account", emailBody);

                            return RedirectToLocal(returnUrl);
                        }
                    }
                    AddErrors(result);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException)
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
        public ActionResult Index(string sort,string role)
        {
            var model = IndexAction(sort, role);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Index()
        {
            var model = IndexAction(Request["SortBy"], Request["Roles"]);
            return View(model);
        }

        /// <summary>
        /// Action for the account index page (both GET and POST actions)
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private List<EditUserViewModel> IndexAction(string sort, string role)
        {
            // Only users in the SysAdmin role are allowed to delete user accounts
            bool canDelete = Request.GetOwinContext().Authentication.User.IsInRole(MOBOT.BHL.AdminWeb.Helper.SecurityRole.BHLAdminSysAdmin.ToString());

            // Set up the sort variables
            string sortBy = String.IsNullOrWhiteSpace(sort) ? "lname" : sort;
            ViewBag.SortBy = sortBy;
            ViewBag.LNameSort = "lname";
            ViewBag.FNameSort = "fname";
            ViewBag.UNameSort = "uname";
            ViewBag.EmailSort = "email";
            ViewBag.InstitutionSort = "institution";
            ViewBag.GroupSort = "group";
            ViewBag.DisabledSort = "disabled";
            ViewBag.LastLoginSort = "login";

            // Set up the filter variable
            int selectedRole = String.IsNullOrWhiteSpace(role) ? _roleAny : Convert.ToInt32(role);
            ViewBag.SelectedRole = selectedRole;

            var Db = new ApplicationDbContext();
            var users = Db.Users.Where(r => r.Id != 1).OrderBy(r => r.UserName);

            // Populate the Roles dropdown list
            List<CustomRole> roleList = Db.Roles.ToList();
            CustomRole newRole = new CustomRole(" - No Assigned Roles -");
            newRole.Id = _roleNone;
            roleList.Insert(0, newRole);
            newRole = new CustomRole("- Show All Users -");
            newRole.Id = _roleAny;
            roleList.Insert(0, newRole);
            ViewBag.Roles = new SelectList(roleList, "Id", "Name", selectedRole);

            List<EditUserViewModel> model = new List<EditUserViewModel>();
            foreach (var user in users)
            {
                CustomUserRole[] userRoleList = user.Roles.ToArray();

                var u = new EditUserViewModel(user);
                u.AllowDelete = canDelete;
                u.HasRoles = (userRoleList.Count() > 0);

                if (selectedRole == _roleAny)
                {
                    model.Add(u);
                }
                else if (selectedRole == _roleNone && !u.HasRoles)
                {
                    model.Add(u);
                }
                else
                {
                    foreach (CustomUserRole userRole in userRoleList)
                    {
                        if (userRole.RoleId == selectedRole) model.Add(u);
                    }
                }
            }

            // Sort the list as specified
            switch (sortBy)
            {
                case "lname_desc":
                    model = model.OrderByDescending(u => u.LastName).ToList();
                    break;
                case "fname_desc":
                    model = model.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "fname":
                    model = model.OrderBy(u => u.FirstName).ToList();
                    ViewBag.FNameSort = sortBy + "_desc";
                    break;
                case "uname_desc":
                    model = model.OrderByDescending(u => u.UserName).ToList();
                    break;
                case "uname":
                    model = model.OrderBy(u => u.UserName).ToList();
                    ViewBag.UNameSort = sortBy + "_desc";
                    break;
                case "email_desc":
                    model = model.OrderByDescending(u => u.Email).ToList();
                    break;
                case "email":
                    model = model.OrderBy(u => u.Email).ToList();
                    ViewBag.EmailSort = sortBy + "_desc";
                    break;
                case "institution_desc":
                    model = model.OrderByDescending(u => u.HomeInstitutionName).ToList();
                    break;
                case "institution":
                    model = model.OrderBy(u => u.HomeInstitutionName).ToList();
                    ViewBag.InstitutionSort = sortBy + "_desc";
                    break;
                case "group_desc":
                    model = model.OrderByDescending(u => u.InstitutionGroupName).ToList();
                    break;
                case "group":
                    model = model.OrderBy(u => u.InstitutionGroupName).ToList();
                    ViewBag.GroupSort = sortBy + "_desc";
                    break;
                case "disabled_desc":
                    model = model.OrderByDescending(u => u.Disabled).ToList();
                    break;
                case "disabled":
                    model = model.OrderBy(u => u.Disabled).ToList();
                    ViewBag.DisabledSort = sortBy + "_desc";
                    break;
                case "login_desc":
                    model = model.OrderByDescending(u => u.LastLoginDateUtc).ToList();
                    break;
                case "login":
                    model = model.OrderBy(u => u.LastLoginDateUtc).ToList();
                    ViewBag.LastLoginSort = sortBy + "_desc";
                    break;
                default:    // lname
                    model = model.OrderBy(u => u.LastName).ToList();
                    ViewBag.LNameSort = sortBy + "_desc";
                    break;
            }

            return model;
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult DownloadList()
        {
            List<List<Tuple<string, object>>> rows = new BHLProvider().AspNetUserSelectAll();

            // Accumulate the CSV data
            var records = new List<dynamic>();
            foreach (List<Tuple<string, object>> row in rows)
            {
                dynamic record = new ExpandoObject();
                for (int x = 0; x < row.Count; x++)
                {
                    if (row[x].Item1 == "LockoutEndDateUtc")
                    {
                        AddCsvProperty(record,
                            "Locked",
                            row[x].Item2 == null ? "" : (DateTime.Compare((DateTime)row[x].Item2, DateTime.Now.ToUniversalTime()) > 0 ? "X" : ""));
                    }
                    else
                    {
                        AddCsvProperty(record,
                            row[x].Item1.EndsWith("Utc") ? row[x].Item1.Substring(0, row[x].Item1.Length - 3) : row[x].Item1,
                            row[x].Item2);
                    }
                }
                records.Add(record);
            }

            // Convert the output to a byte array of a CSV string
            byte[] downloadString = new CSV().FormatCSVData(records);

            // Write the byte array to the output stream
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = string.Format("UserAccounts{0}.csv", System.DateTime.Now.ToString("yyyyMMddHHmmss")),
                Inline = false,  // prompt the user for downloading, set true to show the file in the browser
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(downloadString, "text/csv");
        }

        private void AddCsvProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult Edit(string id, ManageMessageId? Message = null)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            var model = new EditUserViewModel(user);
            ViewBag.MessageId = Message;
            ViewBag.Institutions = GetInstitutions();
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
                user.HomeInstitutionCode = model.HomeInstitutionCode;
                bool wasDisabled = false;
                if (model.Disabled && !user.Disabled) wasDisabled = true;   // Flag users being disabled
                user.Disabled = model.Disabled;
                Db.Entry(user).State = System.Data.Entity.EntityState.Modified;

                IdentityResult result = await UserManager.UserValidator.ValidateAsync(user);
                if (result.Succeeded)
                {
                    await Db.SaveChangesAsync();
                    // If user was just disabled, remove from all roles
                    if (wasDisabled)
                    {
                        var idManager = new IdentityManager();
                        idManager.ClearUserRoles(user.Id);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                    ViewBag.Institutions = GetInstitutions();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public IEnumerable<SelectListItem> GetInstitutions()
        {
            List<SelectListItem> institutions = new Server.BHLProvider().InstituationSelectAll()
                .OrderBy(n => n.InstitutionName)
                .Select(n => new SelectListItem
                {
                    Value = n.InstitutionCode,
                    Text = n.InstitutionName
                }).ToList();

            var institutionTip = new SelectListItem() { Value = null, Text = "--- select content provider ---" };
            institutions.Insert(0, institutionTip);
            return new SelectList(institutions, "Value", "Text");
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
        public ActionResult Unlock(string id = null)
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

        [HttpPost, ActionName("Unlock")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "BHL.Admin.Admin, BHL.Admin.SysAdmin")]
        public ActionResult UnlockConfirmed(string id)
        {
            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == id);
            UserManager.SetLockoutEndDate(user.Id, DateTime.UtcNow.AddMinutes(-1));
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

            // Update the last login date
            var Db = new ApplicationDbContext();
            var updateUser = Db.Users.First(u => u.Id == user.Id);
            updateUser.LastLoginDateUtc = DateTime.Now.ToUniversalTime();
            Db.Entry(updateUser).State = System.Data.Entity.EntityState.Modified;
            await Db.SaveChangesAsync();
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