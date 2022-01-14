using Codecool.Shop.Data.Infrastructure.Repository;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Infrastructure;

public class UnitOfWork: IUnitOfWork
{
    public ShopDbContext DbContext { private get; init; }

    public IGenericDbRepository<ProductCategory> Categories
        => new GenericDbRepository<ProductCategory>();
    public IGenericDbRepository<Product> Products
        => new GenericDbRepository<Product>();
    public IGenericDbRepository<Supplier> Suppliers
        => new GenericDbRepository<Supplier>();
    public IGenericDbRepository<Cart> Carts
        => new GenericDbRepository<Cart>();
    public IGenericDbRepository<CartItem> CartItems
        => new GenericDbRepository<CartItem>();
    public IGenericDbRepository<Order> Orders
        => new GenericDbRepository<Order>();
    public IGenericDbRepository<Client> Clients
        => new GenericDbRepository<Client>();

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Save() => DbContext.SaveChanges();
}