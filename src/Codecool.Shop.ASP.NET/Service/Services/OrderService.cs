using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class OrderService : IOrderService
{
    public IUnitOfWork UnitOfWork { private get; init; }

    public Cart GetShoppingCartByUserId(string userId)
        => UnitOfWork.Carts.Get(x => x.UserId == userId);

    public IEnumerable<int> GetProductsIdsByUserId(string userId)
        => GetCartItemsByUserId(userId).Select(cartItem => cartItem.ProductId);


    public double GetTotalPrice(string userId)
        => GetCartItemsByUserId(userId).Select
            (cartItem => cartItem.Product.DefaultPrice * cartItem.Quantity).Sum();


    public IEnumerable<CartItem> GetCartItemsByUserId(string userId)
    {
        Cart cart = GetShoppingCartByUserId(userId);

        IEnumerable<CartItem> cartItems =
            UnitOfWork.CartItems.GetRange(x => x.CartId == cart.Id);

        return GetProductsForImportedCartItems(cartItems);
    }

    private IEnumerable<CartItem> GetProductsForImportedCartItems(IEnumerable<CartItem> cartItems)
    {
        foreach (CartItem cartItem in cartItems)
        {
            cartItem.Product = UnitOfWork.Products.Get(x => x.Id == cartItem.ProductId);
        }

        return cartItems;
    }

    public void AddOrder(Order item)
    {
        UnitOfWork.Orders.Add(item);
        UnitOfWork.Save();
    }

    public void ClearCartAndCartItems(string userId)
    {
        Cart cartToRemove = UnitOfWork.Carts.Get
            (x => x.UserId == userId);
        IEnumerable<CartItem> cartItemsToRemove = UnitOfWork.CartItems.GetRange
            (x => x.CartId == cartToRemove.Id);

        UnitOfWork.CartItems.RemoveRange(cartItemsToRemove);
        UnitOfWork.Carts.Remove(cartToRemove);
        UnitOfWork.Save();
    }

    public Order GetOrder(string userId)
        => UnitOfWork.Orders.Get(x => x.UserId == userId);

    public void Modify(Order orderToUpdate)
    {
        UnitOfWork.Orders.Modify(orderToUpdate);
        UnitOfWork.Save();
    }
}