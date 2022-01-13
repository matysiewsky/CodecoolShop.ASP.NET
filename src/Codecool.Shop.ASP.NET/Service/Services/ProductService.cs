using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ProductService : IProductService
{
    public IProductRepository ProductRepository { get; init; }
    public IProductCategoryRepository ProductCategoryRepository { get; init; }
    public ISupplierService SupplierService { get; init; }

    public ProductCategory GetProductCategory(int categoryId) => ProductCategoryRepository.Get(categoryId);

    public IEnumerable<Product> GetAllProducts() => ProductRepository.GetAll();

    public IEnumerable<ProductCategory> GetAllProductCategories() => ProductCategoryRepository.GetAll();

    public IEnumerable<Product> GetProductsForCategory(int categoryId)
    {

        if (ProductCategoryRepository.Get(categoryId) != null)
        {
            ProductCategory category = ProductCategoryRepository.Get(categoryId);

            return ProductRepository.GetAllBy(category);
        }

        return Enumerable.Empty<Product>();
    }


    public IEnumerable<Product> GetProductsForSupplier(int supplierId)
    {

        if (SupplierService.GetSupplier(supplierId) != null)
        {
            Supplier supplier = SupplierService.GetSupplier(supplierId);

            return ProductRepository.GetAllBy(supplier);
        }

        return Enumerable.Empty<Product>();
    }

    public Product GetProduct(int productId) => ProductRepository.Get(productId);
}