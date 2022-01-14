using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ProductCategoryDbRepository : IProductCategoryRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public ProductCategory Add(ProductCategory item)
    {
        ShopDbContext.Categories.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(ProductCategory item)
    {
        ShopDbContext.Categories.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public ProductCategory Get(int id) =>
        ShopDbContext.Categories.FirstOrDefault(x => x.Id == id);

    public IEnumerable<ProductCategory> GetAll() => ShopDbContext.Categories;
}