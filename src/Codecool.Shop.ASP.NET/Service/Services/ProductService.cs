using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ProductService : IProductService
{
    public IUnitOfWork UnitOfWork { private get; init; }

    public ProductCategory GetProductCategory(int categoryId)
        => UnitOfWork.Categories.Get(x => x.Id == categoryId);

    public IEnumerable<Product> GetAllProducts()
        => UnitOfWork.Products.GetAll();

    public IEnumerable<ProductCategory> GetAllProductCategories()
        => UnitOfWork.Categories.GetAll();

    public IEnumerable<Product> GetProductsForCategory(int categoryId)
    {
        ProductCategory category
            = UnitOfWork.Categories.Get(x => x.Id == categoryId);

        return (category != null) ?
            // ReSharper disable once PossibleUnintendedReferenceComparison
            UnitOfWork.Products.GetRange(x => x.ProductCategory == category)
            : Enumerable.Empty<Product>();
    }

    public IEnumerable<Product> GetProductsForSupplier(int supplierId)
    {
        Supplier supplier = UnitOfWork.Suppliers.Get(x => x.Id == supplierId);

        return (supplier != null) ?
            UnitOfWork.Products.GetRange(x => x.Supplier == supplier)
            : Enumerable.Empty<Product>();
    }

    public Product GetProduct(int productId)
        => UnitOfWork.Products.Get(x => x.Id == (productId));
}