using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codecool.Shop.Data.Repositories.Repositories;

public class CartItemDbRepository: ICartItemRepository
{
    public AppDbContext AppDbContext { get; init; }

    public CartItem Add(CartItem item)
    {
        AppDbContext.CartItems.Add(item);
        AppDbContext.SaveChanges();

        return item;
    }

    public void Remove(CartItem item)
    {
        AppDbContext.CartItems.Remove(item);
        AppDbContext.SaveChanges();
    }

    public CartItem Get(int id) => AppDbContext.CartItems.FirstOrDefault(x => x.Id == id);

    public IEnumerable<CartItem> GetAll(int shoppingCartId) =>
        AppDbContext.CartItems.Where(
            c => c.CartId == shoppingCartId);

    public void RemoveCartItems(int cartId)
    {
        AppDbContext.CartItems.RemoveRange(AppDbContext.CartItems.Where(x => x.CartId == cartId));
    }

    public IEnumerable<CartItem> GetAll() => AppDbContext.CartItems;

    public void Update(int itemId)
    {
        CartItem itemToUpdate = Get(itemId);
        itemToUpdate.Quantity++;
        AppDbContext.SaveChanges();
    }

    public void Modify(CartItem itemToUpdate)
    {
        AppDbContext.Attach(itemToUpdate);
        AppDbContext.Entry(itemToUpdate).State = EntityState.Modified;
        AppDbContext.SaveChanges();
    }
}