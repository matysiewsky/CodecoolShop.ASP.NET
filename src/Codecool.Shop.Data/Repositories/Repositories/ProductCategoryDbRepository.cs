using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class ProductCategoryDbRepository : IProductCategoryRepository
{
    public AppDbContext AppDbContext { get; init; }

    public ProductCategory Add(ProductCategory item)
    {
        AppDbContext.Categories.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(ProductCategory item)
    {
        AppDbContext.Categories.Remove(item);
        AppDbContext.SaveChanges();
    }

    public ProductCategory Get(int id) =>
        AppDbContext.Categories.FirstOrDefault(x => x.Id == id);

    public IEnumerable<ProductCategory> GetAll() => AppDbContext.Categories;
}