using ComicBookShop.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicBookShop.DataLayer;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    public class BrandsController : Controller
    {
        // GET: Admin/Brands
        public ActionResult Index()
        {
            CompanyDbContext db = new CompanyDbContext();
            List<Publisher> brands = db.Publishers.ToList();
            return View(brands);
        }
    }
}