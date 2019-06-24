using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using EFDbFirstApproachExample.Identity;

[assembly: OwinStartup(typeof(EFDbFirstApproachExample.Startup))]

namespace EFDbFirstApproachExample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);


            //Create Admin Role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            //Create Admin User 
            if (userManager.FindByName("Venko") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Venko";
                user.Email = "venko.trajkovski@gmail.com";
                string userPassword = "admin123";
                var checkUser = userManager.Create(user, userPassword);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            //Create Manager Role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            //Create Manager User
            if (userManager.FindByName("Infidel")==null)
            {
                var user = new ApplicationUser();
                user.UserName = "Infidel";
                user.Email = "infidel756@gmail.com";
                string userPassword = "manager123";
                var checkUser = userManager.Create(user, userPassword);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }

            //Create Customer Role
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}
