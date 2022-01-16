using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface IProductService
{
    Product GetProduct(int productId);
    IEnumerable<Product> GetProducts();
    IEnumerable<ProductCategory> GetProductCategories();
    IEnumerable<Product> GetProductsForCategory(int categoryId);
    IEnumerable<Product> GetProductsForSupplier(int supplierId);

}