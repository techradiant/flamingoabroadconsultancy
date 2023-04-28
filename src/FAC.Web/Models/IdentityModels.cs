using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FAC.Web.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FAC.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
        // Your Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>// DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }


        public void PerformInitialSetup(ApplicationDbContext context)
        {
            IdentityHelper.PerformInitialSetup(context);
        }
    }
}
#region Helpers
namespace FAC.Web
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }

        public static ApplicationUser GetCurrentUser(string currentUserName)
        {
            ApplicationUser user = null;
            //HttpContext.Current.User.Identity.GetUserName()
            if (!string.IsNullOrWhiteSpace(currentUserName))
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                    {
                        user = userManager.FindByName(currentUserName);
                        if (user == null)
                        {
                            user = userManager.FindByEmail(currentUserName);
                        }
                    }
                }
            }
            return user;
        }
        public static void PerformInitialSetup(ApplicationDbContext context)
        {
            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
            {
                using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    //system Role
                    if (!roleManager.RoleExists("admin"))
                    {
                        roleManager.Create(new IdentityRole("admin"));
                    }

                    //system Role
                    if (!roleManager.RoleExists("user"))
                    {
                        roleManager.Create(new IdentityRole("user"));
                    }

                    ApplicationUser admin = userManager.FindByName("admin");

                    if (admin == null)
                    {
                        admin = new ApplicationUser() { UserName = "admin", FirstName = "admin", Email = "info@flamingoabroadconsultancy.com", EmailConfirmed = true, LockoutEnabled = false, SecurityStamp = Guid.NewGuid().ToString("D") };

                        if (userManager.Create(admin, "Admin@1234") != IdentityResult.Success)
                        {
                            throw new Exception("failed");
                        }
                    }
                    #region "Create extra default roles if required at initial setup"
                    ApplicationUser user = userManager.FindByName("user");
                    if (user == null)
                    {
                        user = new ApplicationUser() { UserName = "user", FirstName = "user 1", Email = "user@flamingoabroadconsultancy.com", EmailConfirmed = true, LockoutEnabled = false, SecurityStamp = Guid.NewGuid().ToString("D") };
                        if (userManager.Create(user, "User@1234") != IdentityResult.Success)
                        {
                            throw new Exception("Failed");
                        }
                    }
                    #endregion
                    // role
                    userManager.AddToRole(admin.Id, "admin");
                    userManager.AddToRole(user.Id, "user");

                    context.SaveChanges();
                }
            }
        }
    }
}
#endregion
