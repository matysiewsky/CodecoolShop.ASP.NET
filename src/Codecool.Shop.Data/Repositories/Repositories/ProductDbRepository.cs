using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ProductDbRepository : IProductRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public Product Add(Product item)
    {
        ShopDbContext.Products.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(Product item)
    {
        ShopDbContext.Products.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public Product Get(int id) => ShopDbContext.Products.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Product> GetAll() => ShopDbContext.Products;

    public IEnumerable<Product> GetAllBy(Supplier supplier) =>
        ShopDbContext.Products.Where(x => x.Supplier.Id == supplier.Id);

    public IEnumerable<Product> GetAllBy(ProductCategory productCategory) =>
        ShopDbContext.Products.Where(x => x.ProductCategory.Id == productCategory.Id);
}