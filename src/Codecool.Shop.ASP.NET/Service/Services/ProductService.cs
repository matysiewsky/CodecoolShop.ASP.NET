using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ProductService : IProductService
{
    public IGenericDbRepository<Product> ProductRepository { get; init; }
    public IGenericDbRepository<ProductCategory> ProductCategoryRepository { get; init; }
    public ISupplierService SupplierService { get; init; }

    public ProductCategory GetProductCategory(int categoryId)
        => ProductCategoryRepository.Get(x => x.Id == categoryId);

    public IEnumerable<Product> GetAllProducts()
        => ProductRepository.GetAll();

    public IEnumerable<ProductCategory> GetAllProductCategories()
        => ProductCategoryRepository.GetAll();

    public IEnumerable<Product> GetProductsForCategory(int categoryId)
    {
        ProductCategory category
            = ProductCategoryRepository.Get(x => x.Id == categoryId);

        return (category != null) ?
            // ReSharper disable once PossibleUnintendedReferenceComparison
            ProductRepository.GetRange(x => x.ProductCategory == category)
            : Enumerable.Empty<Product>();
    }

    public IEnumerable<Product> GetProductsForSupplier(int supplierId)
    {
        Supplier supplier = SupplierService.GetSupplier(supplierId);

        return (supplier != null) ?
            ProductRepository.GetRange(x => x.Supplier == supplier)
            : Enumerable.Empty<Product>();
    }

    public Product GetProduct(int productId)
        => ProductRepository.Get(x => x.Id == (productId));
}