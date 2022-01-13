using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class CartDbRepository: ICartRepository
{
    public AppDbContext AppDbContext { get; init; }

    public Cart Add(Cart item)
    {
        AppDbContext.Carts.Add(item);
        AppDbContext.SaveChanges();
        return item;
    }

    public void Remove(Cart item)
    {
        AppDbContext.Carts.Remove(item);
        AppDbContext.SaveChanges();
    }

    public Cart Get(int id) => AppDbContext.Carts.FirstOrDefault(x => x.Id == id);

    public Cart Get(string userId) =>
        AppDbContext.Carts.FirstOrDefault(x => x.UserId == userId);

    public IEnumerable<Cart> GetAll() => AppDbContext.Carts;
}