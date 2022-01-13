using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ProductDbRepository : IProductRepository
{
    public AppDbContext AppDbContext { get; init; }

    public Product Add(Product item)
    {
        AppDbContext.Products.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(Product item)
    {
        AppDbContext.Products.Remove(item);
        AppDbContext.SaveChanges();
    }

    public Product Get(int id) => AppDbContext.Products.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Product> GetAll() => AppDbContext.Products;

    public IEnumerable<Product> GetAllBy(Supplier supplier) =>
        AppDbContext.Products.Where(x => x.Supplier.Id == supplier.Id);

    public IEnumerable<Product> GetAllBy(ProductCategory productCategory) =>
        AppDbContext.Products.Where(x => x.ProductCategory.Id == productCategory.Id);
}