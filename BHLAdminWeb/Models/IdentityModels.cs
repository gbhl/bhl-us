using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOBOT.BHL.AdminWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Disabled { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
    public class BHLUserManager<TUser> : UserManager<TUser> where TUser : class, IUser
    {
        public BHLUserManager(IUserStore<TUser> store)
            : base(store)
        {
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
        public async Task SendEmailAsync(string userId, List<string> ccList, List<string> bccList, string subject, string body)
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

    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            return rm.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var idResult = rm.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var idResult = um.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                um.RemoveFromRole(userId, rm.FindById(role.RoleId).Name);
            }
        }
    }
}
