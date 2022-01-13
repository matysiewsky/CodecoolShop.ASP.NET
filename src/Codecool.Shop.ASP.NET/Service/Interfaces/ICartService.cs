using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface ICartService
{

    Cart ReturnNewCart(string userId);
    void AddToCart(string userId, int productId);
    string GetCartId();
    Cart GetCart(int id);
    public CartItem GetCartItem(int id);
    Cart GetCartByUserId(string userId);
    IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId);
    public int GetCartItemsCount(string userId);
    public void ClearCartItem(CartItem item);
    void ClearCartAndCartItems(Cart cartToClear);
    public void Modify(CartItem orderToUpdate);

}