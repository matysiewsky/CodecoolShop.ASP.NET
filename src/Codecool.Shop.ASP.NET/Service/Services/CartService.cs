using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codecool.Shop.ASP.NET.Service.Interfaces;
using Codecool.Shop.Domain.Models;
using Codecool.Shop.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Codecool.Shop.ASP.NET.Service.Services;

public class CartService : ICartService
{
    public IUnitOfWork UnitOfWork { private get; init; }
    public IHttpContextAccessor HttpContextAccessor { get; init; }

    private const string CartSessionKey = "CartId";

    public Cart ReturnNewCart(string userId)
    {
        UnitOfWork.Carts.Add(new Cart
        {
            UserId = userId,
        });

        return UnitOfWork.Carts.Get(x => x.UserId == userId);
    }

    public void AddToCart(string userId, int productId)
    {

        Product productToAdd = UnitOfWork.Products.Get(x => x.Id == productId);
        Cart cart = UnitOfWork.Carts.Get(x => x.UserId == userId);

        if (cart == null)
        {
            UnitOfWork.Carts.Add(new Cart
            {
                UserId = userId,
            });

            cart = UnitOfWork.Carts.Get(x => x.UserId == userId);
        }

        IEnumerable<CartItem> shoppingCartItems = UnitOfWork.CartItems.GetRange
            (x=> x.CartId == cart.Id);

        foreach (CartItem shoppingCartItem in shoppingCartItems)
        {
            shoppingCartItem.Product = UnitOfWork.Products.Get
                (x => x.Id == shoppingCartItem.ProductId);
        }

        CartItem cartItem = shoppingCartItems.SingleOrDefault(
            cartItem => cartItem.Product.Id == productId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                Product = productToAdd,
                CartId = cart.Id,
                Quantity = 1,
                DateCreated = DateTime.Now,
                ProductId = productToAdd.Id,
            };

            UnitOfWork.CartItems.Add(cartItem);
        }
        else
        {
            UnitOfWork.CartItems.Modify(cartItem);
        }
    }

    public string GetCartId()
        => HttpContextAccessor.HttpContext.Session.Id;

    public Cart GetCart(int id)
        => UnitOfWork.Carts.Get(x=> x.Id == id);
    public CartItem GetCartItem(int id)
        => UnitOfWork.CartItems.Get(x => x.Id == id);
    public Cart GetCartByUserId(string userId)
        => UnitOfWork.Carts.Get(x => x.UserId == userId);

    public IEnumerable<CartItem> GetCartItemsByShoppingCartId(int shoppingCartId)
        => UnitOfWork.CartItems.GetRange(x => x.CartId == shoppingCartId);

    public int GetCartItemsCount(string userId)
    {
        int id = GetCartByUserId(userId).Id;
        return GetCartItemsByShoppingCartId(id).Count();
    }

    public void ClearCartItem(CartItem item)
    {
        UnitOfWork.CartItems.Remove(item);
    }
    public void Modify(CartItem orderToUpdate)
    {
        UnitOfWork.CartItems.Modify(orderToUpdate);
    }
}
