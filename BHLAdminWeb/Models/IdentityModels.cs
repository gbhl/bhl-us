using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOBOT.BHL.AdminWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Disabled { get; set; }

        //public int uid { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(BHLUserManager manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        } 
    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public int DisplaySequence { get; set; }

        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    } 

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, 
        CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("BHLUser")
        {

        }
    }

    /// <summary>
    /// BHLUserManager extends UserManager with additional BHL functionality
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class BHLUserManager : UserManager<ApplicationUser, int>// where TUser : class, IUser
    {
        public BHLUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
            this.UserValidator = new BHLUserValidator(this);
        }

        public static BHLUserManager Create(IdentityFactoryOptions<BHLUserManager> options, IOwinContext context)  
        { 
            return new BHLUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));
        } 

        public new MVCServices.IBHLIdentityMessageService EmailService 
        {
            get;
            set;
        }

        /// <summary>
        /// Allow emails to user to include copies or blind copies
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(int userId, List<string> ccList, List<string> bccList, string subject, string body)
        {
            if (this.EmailService != null)
            {
                IdentityMessage message = new IdentityMessage
                {
                    Destination = await this.GetEmailAsync(userId),
                    Subject = subject,
                    Body = body
                };
                await this.EmailService.SendAsync(message, ccList, bccList);
            }
        }
    }

    /// <summary>
    /// Extend the default UserValidator to recognize the integer primary key and to 
    /// require unique email address for each user account.
    /// </summary>
    public class BHLUserValidator : UserValidator<ApplicationUser, int>
    {
        public BHLUserValidator(BHLUserManager manager) : base(manager)
        {
            this.RequireUniqueEmail = true;
        }
    }

    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<CustomRole, int>(
                new CustomRoleStore(new ApplicationDbContext()));
            return rm.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var rm = new RoleManager<CustomRole, int>(
                new CustomRoleStore(new ApplicationDbContext()));
            var idResult = rm.Create(new CustomRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = new BHLUserManager(
                new UserStore<ApplicationUser, CustomRole, int,
                CustomUserLogin, CustomUserRole, CustomUserClaim>(new ApplicationDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(int userId, string roleName)
        {
            var um = new BHLUserManager(
                new UserStore<ApplicationUser, CustomRole, int,
                CustomUserLogin, CustomUserRole, CustomUserClaim>(new ApplicationDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(int userId)
        {
            var um = new BHLUserManager(
                new UserStore<ApplicationUser, CustomRole, int,
                CustomUserLogin, CustomUserRole, CustomUserClaim>(new ApplicationDbContext()));
            var rm = new RoleManager<CustomRole, int>(
                new CustomRoleStore(new ApplicationDbContext()));

            var user = um.FindById(userId);
            var currentRoles = new List<CustomUserRole>();
            currentRoles.AddRange(user.Roles);

            foreach(var role in currentRoles)
            {
                um.RemoveFromRole(userId, rm.FindById(role.RoleId).Name);

            }
        }
    }
}
