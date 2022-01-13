using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class SupplierDbRepository : ISupplierRepository
{
    public AppDbContext AppDbContext { get; init; }

    public Supplier Add(Supplier item)
    {
        AppDbContext.Suppliers.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(Supplier item)
    {
        AppDbContext.Suppliers.Remove(item);
        AppDbContext.SaveChanges();
    }

    public Supplier Get(int id) => AppDbContext.Suppliers.FirstOrDefault(x => x.Id == id);

    public IEnumerable<Supplier> GetAll() => AppDbContext.Suppliers;
}