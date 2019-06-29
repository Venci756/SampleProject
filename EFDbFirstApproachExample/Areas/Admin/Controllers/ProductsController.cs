﻿using ComicBookShop.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComicBookShop.DataLayer;
using ComicBookShop.ServiceContracts;
using ComicBookShop.ServiceLayer;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private CompanyDbContext _db;
        IProductsService productsService;
        public ProductsController()
        {
            _db = new CompanyDbContext();
            productsService = new ProductsService();
        }
        [ValidateInput(false)]
        public ActionResult Index(string search = "", string SortColumn = "ProductName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            ViewBag.search = search;
            //CompanyDbContext db = new CompanyDbContext();
            List<Product> products = productsService.SearchProducts(search);

            // Sorting
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.ProductID).ToList();
                }
                else products = products.OrderByDescending(x => x.ProductID).ToList();
            }
            else if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.ProductName).ToList();
                }
                else products = products.OrderByDescending(x => x.ProductName).ToList();
            }
            else if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.Price).ToList();
                }
                else products = products.OrderByDescending(x => x.Price).ToList();
            }
            else if (ViewBag.SortColumn == "DateOfPurpose")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.DateOfPurpose).ToList();
                }
                else products = products.OrderByDescending(x => x.DateOfPurpose).ToList();
            }
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.AvailabilityStatus).ToList();
                }
                else products = products.OrderByDescending(x => x.AvailabilityStatus).ToList();
            }
            else if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.CategoryID).ToList();
                }
                else products = products.OrderByDescending(x => x.CategoryID).ToList();
            }
            else if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.PublisherID).ToList();
                }
                else products = products.OrderByDescending(x => x.PublisherID).ToList();
            }
            else if (ViewBag.SortColumn == "Active")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                {
                    products = products.OrderBy(x => x.Active).ToList();
                }
                else products = products.OrderByDescending(x => x.Active).ToList();
            }

            //foreach (var item in products)
            //{
            //    string imageDataURL = string.Format("data:image;base64,{0}", item.Photo);
            //    ViewBag.ImageData = imageDataURL;
            //}

            //paging
            int NoOfRecordsPerPage = 3;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfPagesToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfPagesToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(products);
        }

        public ActionResult Details(long id)
        {
            //CompanyDbContext db = new CompanyDbContext();
            Product p = productsService.GetProductByID(id);
            return View(p);
        }

        [ChildActionOnly]
        public ActionResult MyActionThatGeneratesAPartial(string parameter1)
        {

            var partialViewModel = _db.Products.ToList();
            //List<Product> products = db.Products.Where(x => x.ProductName.Contains(search)).ToList();
            return PartialView(partialViewModel);
        }

        public ActionResult Create()
        {
 
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Publishers = _db.Publishers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "ProductID, ProductName, Price, Authors, DateOfPurpose, AvailabilityStatus, CategoryID, PublisherID, Active, Image")]Product p)
        {
            if (ModelState.IsValid)
            {
                //CompanyDbContext db = new CompanyDbContext();
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    p.Image = base64String;                                                             //
                    string imageDataURL = string.Format("data:image/jpg;base64,{0}", p.Image);          //
                    ViewBag.ImageData = imageDataURL;
                }
                productsService.InsertProduct(p);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = _db.Categories.ToList();
                ViewBag.Publishers = _db.Publishers.ToList();
                return View();
            }
        }
        public ActionResult Edit(long id)
        {

            Product existingProduct = productsService.GetProductByID(id);
            ViewBag.Categories = _db.Categories.ToList();
            ViewBag.Publishers = _db.Publishers.ToList();
            return View(existingProduct);
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            //CompanyDbContext db = new CompanyDbContext();
            if (ModelState.IsValid)
            {
                Product existingProduct = _db.Products.Where(x => x.ProductID == p.ProductID).FirstOrDefault();
                existingProduct.ProductName = p.ProductName;
                existingProduct.Price = p.Price;
                existingProduct.DateOfPurpose = p.DateOfPurpose;
                existingProduct.CategoryID = p.CategoryID;
                existingProduct.PublisherID = p.PublisherID;
                existingProduct.AvailabilityStatus = p.AvailabilityStatus;
                existingProduct.Active = p.Active;
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    //p.Image = base64String;
                    existingProduct.Image = base64String;
                    string imageDataURL = string.Format("data:image/jpg;base64,{0}", p.Image);          
                    ViewBag.ImageData = imageDataURL;
                }
                //existingProduct.Image = p.Image;

                productsService.UpdateProduct(existingProduct);
            }
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Delete(long id)
        {

            Product product = productsService.GetProductByID(id);
            _db.SaveChanges();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(long id, Product p)    
        {
            productsService.DeleteProduct(id);
            return RedirectToAction("Index", "Products");
        }
    }
}