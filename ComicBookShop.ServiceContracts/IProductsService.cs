using System;
using System.Collections.Generic;
using ComicBookShop.DomainModels;

namespace ComicBookShop.ServiceContracts
{
    public interface IProductsService
    {
        List<Product> GetProducts();
        List<Product> SearchProducts(String ProductName);
        Product GetProductByID(long ProductID);
        void InsertProduct(Product p);
        void UpdateProduct(Product p);
        void DeleteProduct(long ProductID);
    }
}
