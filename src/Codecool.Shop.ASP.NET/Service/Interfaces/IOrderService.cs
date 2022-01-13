using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface IOrderService
{
    Cart GetShoppingCartByUserId(string userId);
    IEnumerable<int> GetProductsIdsByUserId(string userId);
    public IEnumerable<CartItem> GetCartItemsByUserId(string userId);

    double GetTotalPrice(string userId);
    void AddOrder(Order item);
    void ClearCartAndCartItems(string userId);

    Order GetOrder(string userId);

    public void Modify(Order orderToUpdate);
}