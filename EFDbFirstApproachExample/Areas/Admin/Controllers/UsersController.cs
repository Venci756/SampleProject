using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Identity;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Users
        public ActionResult Index()
        {
            List<ApplicationUser> existingUsers = db.Users.ToList();
            return View(existingUsers);
        }
    }
}