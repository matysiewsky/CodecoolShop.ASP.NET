using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class OrderService : IOrderService
{
    public IGenericDbRepository<Order> OrderRepository { get; init; }
    public ICartService CartService { get; init; }
    public IProductService ProductService { get; init; }

    public Cart GetShoppingCartByUserId(string userId)
        => CartService.GetCartByUserId(userId);

    public IEnumerable<int> GetProductsIdsByUserId(string userId)
        => GetCartItemsByUserId(userId).Select(cartItem => cartItem.ProductId);


    public double GetTotalPrice(string userId)
        => GetCartItemsByUserId(userId).Select
            (cartItem => cartItem.Product.DefaultPrice * cartItem.Quantity).Sum();


    public IEnumerable<CartItem> GetCartItemsByUserId(string userId)
    {
        Cart cart = GetShoppingCartByUserId(userId);

        IEnumerable<CartItem> cartItems =
            CartService.GetCartItemsByShoppingCartId(GetShoppingCartByUserId(userId).Id);

        return GetProductsForImportedCartItems(cartItems);
    }

    private IEnumerable<CartItem> GetProductsForImportedCartItems(IEnumerable<CartItem> cartItems)
    {
        foreach (CartItem cartItem in cartItems)
        {
            cartItem.Product = ProductService.GetProduct(cartItem.ProductId);
        }

        return cartItems;
    }

    public void AddOrder(Order item)
        => OrderRepository.Add(item);

    public void ClearCartAndCartItems(string userId)
    {
        Cart cartToClear = CartService.GetCartByUserId(userId);
        CartService.ClearCartAndCartItems(cartToClear);
    }

    public Order GetOrder(string userId)
        => OrderRepository.Get(x => x.UserId == userId);

    public void Modify(Order orderToUpdate)
        => OrderRepository.Modify(orderToUpdate);
}