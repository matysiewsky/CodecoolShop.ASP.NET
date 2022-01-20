using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class OrderService : IOrderService
{
    private IUnitOfWork UnitOfWork { get; }

    public OrderService(IUnitOfWork unitOfWork)
        => UnitOfWork = unitOfWork;


    private Cart GetCart(string userId)
        => UnitOfWork.Carts.Get(x => x.UserId == userId);
    public IEnumerable<CartItem> GetCartItems(string userId)
    {
        Cart cart = GetCart(userId);

        return UnitOfWork.CartItems.GetRange(x => x.CartId == cart.Id)
            .Join(UnitOfWork.Products.GetAll(), cartItem => cartItem.Product.Id, product => product.Id,
                (cartItem, product) => new CartItem
                {
                    CartId = cartItem.CartId,
                    DateCreated = cartItem.DateCreated,
                    Id = cartItem.Id,
                    Product = product,
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity
                })
            .ToList();
    }
    public IEnumerable<int> GetProductsIds(string userId)
        => GetCartItems(userId).Select(cartItem => cartItem.ProductId);

    public double GetTotalPrice(string userId)
        => GetCartItems(userId).Select
            (cartItem => cartItem.Product.DefaultPrice * cartItem.Quantity).Sum();
    public Order GetOrder(string userId)
        => UnitOfWork.Orders.Get(x => x.UserId == userId);
    public void AddOrder(Order item)
    {
        UnitOfWork.Orders.Add(item);
        UnitOfWork.Save();
    }
    public void RemoveCart(string userId)
    {
        Cart cartToRemove = UnitOfWork.Carts.Get
            (x => x.UserId == userId);

        UnitOfWork.Carts.Remove(cartToRemove);
        UnitOfWork.Save();
    }

    public void RemoveCartItems(string userId)
    {
        int cartId = GetCart(userId).Id;

        IEnumerable<CartItem> cartItemsToRemove = UnitOfWork.CartItems.GetRange
            (x => x.CartId == cartId);

        UnitOfWork.CartItems.RemoveRange(cartItemsToRemove);
        UnitOfWork.Save();
    }

    public void Modify(Order orderToUpdate)
    {
        UnitOfWork.Orders.Modify(orderToUpdate);
        UnitOfWork.Save();
    }
}