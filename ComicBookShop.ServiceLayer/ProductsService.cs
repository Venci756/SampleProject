using System;
using ComicBookShop.ServiceContracts;
using ComicBookShop.DataLayer;
using ComicBookShop.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace ComicBookShop.ServiceLayer
{
    public class ProductsService : IProductsService
    {
        private readonly CompanyDbContext _db;
        public ProductsService()
        {
            this._db = new CompanyDbContext();
        }
       

        public Product GetProductByID(long ProductID)
        {
            Product p = _db.Products.Where(temp => temp.ProductID == ProductID).FirstOrDefault();
            return p;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = _db.Products.ToList();
            return products;
        }

        public void InsertProduct(Product p)
        {
            _db.Products.Add(p);
            _db.SaveChanges();
        }

        public List<Product> SearchProducts(string ProductName)
        {
            List<Product> products = _db.Products.Where(temp => temp.ProductName.Contains(ProductName)).ToList();
            return products;
        }

        public void UpdateProduct(Product p)
        {
            Product existingProduct = _db.Products.Where(x => x.ProductID == p.ProductID).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.DateOfPurpose = p.DateOfPurpose;
            existingProduct.CategoryID = p.CategoryID;
            existingProduct.PublisherID = p.PublisherID;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.Active = p.Active;
            existingProduct.Image = p.Image;
            _db.SaveChanges();
        }
        public void DeleteProduct(long ProductID)
        {
            Product existingProduct = _db.Products.Where(temp => temp.ProductID == ProductID).FirstOrDefault();
            _db.Products.Remove(existingProduct);
            _db.SaveChanges();
        }
    }
}
