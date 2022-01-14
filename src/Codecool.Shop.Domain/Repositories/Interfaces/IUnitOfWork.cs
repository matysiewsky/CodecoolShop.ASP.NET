using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.Domain.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericDbRepository<ProductCategory> Categories { get; }
    IGenericDbRepository<Product> Products { get; }
    IGenericDbRepository<Supplier> Suppliers { get; }
    IGenericDbRepository<Cart> Carts { get; }
    IGenericDbRepository<CartItem> CartItems { get; }
    IGenericDbRepository<Order> Orders { get; }
    IGenericDbRepository<Client> Clients { get; }
    void Save();
}