using System.Collections.Generic;
using Codecool.Shop.Domain.Models;

namespace Codecool.Shop.ASP.NET.Service.Interfaces;

public interface IOrderService
{
    IEnumerable<CartItem> GetCartItems(string userId);
    IEnumerable<int> GetProductsIds(string userId);
    double GetTotalPrice(string userId);
    Order GetOrder(string userId);
    void AddOrder(Order item);
    void RemoveCart(string userId);
    void RemoveCartItems(string userId);
    public void Modify(Order orderToUpdate);
}