using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface ICartService
{
    string GetSessionId();
    Cart GetCart(string userId);
    CartItem GetCartItem(int id);
    IEnumerable<CartItem> GetCartItems(int shoppingCartId);
    int GetCartItemsCount(string userId);
    Cart AddCart(string userId);
    void AddCartItem(string userId, int productId);
    void RemoveCartItem(CartItem item);
    void ModifyCartItem(CartItem orderToUpdate);

}