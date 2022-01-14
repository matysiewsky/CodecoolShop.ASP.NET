using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class CartDbRepository: ICartRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public Cart Add(Cart item)
    {
        ShopDbContext.Carts.Add(item);
        ShopDbContext.SaveChanges();
        return item;
    }

    public void Remove(Cart item)
    {
        ShopDbContext.Carts.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public Cart Get(int id) => ShopDbContext.Carts.FirstOrDefault(x => x.Id == id);

    public Cart Get(string userId) =>
        ShopDbContext.Carts.FirstOrDefault(x => x.UserId == userId);

    public IEnumerable<Cart> GetAll() => ShopDbContext.Carts;
}