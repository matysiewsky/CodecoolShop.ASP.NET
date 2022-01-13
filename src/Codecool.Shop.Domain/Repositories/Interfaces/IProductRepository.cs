using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetAllBy(Supplier supplier);
    IEnumerable<Product> GetAllBy(ProductCategory productCategory);
}