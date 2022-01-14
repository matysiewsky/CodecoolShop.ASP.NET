using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using static Codecool.Shop.Data.Extensions.GenericDbRepositoryExtensions;

namespace Codecool.Shop.Data.Infrastructure.Repository;

public class UnitOfWork: IUnitOfWork
{
    public ShopDbContext DbContext { private get; init; }

    public IGenericDbRepository<ProductCategory> Categories
        => GenericDbRepoFactory<ProductCategory>(DbContext);
    public IGenericDbRepository<Product> Products
        => GenericDbRepoFactory<Product>(DbContext);
    public IGenericDbRepository<Supplier> Suppliers
        => GenericDbRepoFactory<Supplier>(DbContext);
    public IGenericDbRepository<Cart> Carts
        => GenericDbRepoFactory<Cart>(DbContext);
    public IGenericDbRepository<CartItem> CartItems
        => GenericDbRepoFactory<CartItem>(DbContext);
    public IGenericDbRepository<Order> Orders
        => GenericDbRepoFactory<Order>(DbContext);
    public IGenericDbRepository<Client> Clients
        => GenericDbRepoFactory<Client>(DbContext);

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Save() => DbContext.SaveChanges();
}