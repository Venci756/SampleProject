using EFDbFirstApproachExample.Identity;
using EFDbFirstApproachExample.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ComicBookShop.DomainModels;

namespace EFDbFirstApproachExample.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext appDbContext = new ApplicationDbContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var passwordHash = Crypto.HashPassword(model.Password);
                var user = new ApplicationUser() { Email = model.Email, UserName = model.UserName, PasswordHash = passwordHash,Address=model.Address, City = model.City, Birthday = model.DateOfBirth };
             
                IdentityResult result = userManager.Create(user);

                if (result.Succeeded)
                {
                    //role
                    userManager.AddToRole(user.Id, "Customer");


                    //login
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid data");
                    
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser user = userManager.Find(model.UserName, model.Password);
            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

                if (userManager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (userManager.IsInRole(user.Id, "Manager"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Manager" });
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("myerror", "Invalid username or password");
                return View();
            }
        }

        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult MyProfile()
        {
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser appUser =  userManager.FindById(User.Identity.GetUserId());
            return View(appUser);
        }
    }
}