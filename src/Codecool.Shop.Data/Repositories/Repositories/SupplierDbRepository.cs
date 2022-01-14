using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class SupplierDbRepository : ISupplierRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public Supplier Add(Supplier item)
    {
        ShopDbContext.Suppliers.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(Supplier item)
    {
        ShopDbContext.Suppliers.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public Supplier Get(int id) => ShopDbContext.Suppliers.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Supplier> GetAll() => ShopDbContext.Suppliers;
}