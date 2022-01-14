using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class CartItemDbRepository: ICartItemRepository
{
    public ShopDbContext ShopDbContext { get; init; }

    public CartItem Add(CartItem item)
    {
        ShopDbContext.CartItems.Add(item);
        ShopDbContext.SaveChanges();

        return item;
    }

    public void Remove(CartItem item)
    {
        ShopDbContext.CartItems.Remove(item);
        ShopDbContext.SaveChanges();
    }

    public CartItem Get(int id) => ShopDbContext.CartItems.FirstOrDefault(x => x.Id == id);

    public IEnumerable<CartItem> GetAll(int shoppingCartId) =>
        ShopDbContext.CartItems.Where(
            c => c.CartId == shoppingCartId);

    public void RemoveCartItems(int cartId)
    {
        ShopDbContext.CartItems.RemoveRange(ShopDbContext.CartItems.Where(x => x.CartId == cartId));
    }

    public IEnumerable<CartItem> GetAll() => ShopDbContext.CartItems;

    public void Update(int itemId)
    {
        CartItem itemToUpdate = Get(itemId);
        itemToUpdate.Quantity++;
        ShopDbContext.SaveChanges();
    }

    public void Modify(CartItem itemToUpdate)
    {
        ShopDbContext.Attach(itemToUpdate);
        ShopDbContext.Entry(itemToUpdate).State = EntityState.Modified;
        ShopDbContext.SaveChanges();
    }
}