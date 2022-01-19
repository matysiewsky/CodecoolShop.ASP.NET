using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class ProductService : IProductService
{
    private IUnitOfWork UnitOfWork { get; }

    public ProductService(IUnitOfWork unitOfWork)
        => UnitOfWork = unitOfWork;


    public Product GetProduct(int productId)
        => UnitOfWork.Products.Get(x => x.Id == productId);
    public IEnumerable<Product> GetProducts()
        => UnitOfWork.Products.GetAll();

    public IEnumerable<ProductCategory> GetProductCategories()
        => UnitOfWork.Categories.GetAll();

    public IEnumerable<Product> GetProductsForCategory(int categoryId)
    {
        ProductCategory category
            = UnitOfWork.Categories.Get(x => x.Id == categoryId);

        return category != null ?
            UnitOfWork.Products.GetRange(x => x.ProductCategory.Id == category.Id)
            : Enumerable.Empty<Product>();
    }

    public IEnumerable<Product> GetProductsForSupplier(int supplierId)
    {
        Supplier supplier = UnitOfWork.Suppliers.Get(x => x.Id == supplierId);

        return supplier != null ?
            UnitOfWork.Products.GetRange(x => x.Supplier.Id == supplier.Id)
            : Enumerable.Empty<Product>();
    }


}